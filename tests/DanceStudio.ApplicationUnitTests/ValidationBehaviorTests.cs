using DanceStudio.Application.Common.Behavior;
using DanceStudio.Application.Studios.Commands.CreateStudio;
using DanceStudio.Domain.Studios;
using DanceStudio.TestCommon.Studios;
using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;

namespace DanceStudio.ApplicationUnitTests
{
    public class ValidationBehaviorTests
    {
        private readonly ValidationBehavior<CreateStudioCommand, ErrorOr<Studio>> _validationBehavior;
        private readonly IValidator<CreateStudioCommand> _mockValidator;
        private readonly RequestHandlerDelegate<ErrorOr<Studio>> _mockNextBehavior;

        public ValidationBehaviorTests()
        {
            // Create a next behavior (mock)
            _mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<Studio>>>();

            // Create validator (mock)
            _mockValidator = Substitute.For<IValidator<CreateStudioCommand>>();

            // Create validation behavior (SUT)
            _validationBehavior = new ValidationBehavior<CreateStudioCommand, ErrorOr<Studio>>(_mockValidator);
        }

        [Fact]
        public async Task InvokeBehavior_WhenValidatorResultIsValid_ShouldInvokeNextBehavior()
        {
            // Arrange
            var createGymRequest = StudioCommandFactory.CreateCreateStudioCommand();
            var studio = StudioFactory.CreateStudio();

            _mockValidator
                .ValidateAsync(createGymRequest, Arg.Any<CancellationToken>())
                .Returns(new FluentValidation.Results.ValidationResult());

            _mockNextBehavior.Invoke().Returns(studio);

            // Act
            var result = await _validationBehavior.Handle(createGymRequest, _mockNextBehavior, default);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo(studio);
        }

        [Fact]
        public async Task InvokeBehavior_WhenValidatorResultIsNotValid_ShouldReturnListOfErrors()
        {
            // Arrange
            var createStudioRequest = StudioCommandFactory.CreateCreateStudioCommand();
            List<ValidationFailure> validationFailures = [new(propertyName: "property", errorMessage: "property error message")];

            _mockValidator
                .ValidateAsync(createStudioRequest, Arg.Any<CancellationToken>())
                .Returns(new FluentValidation.Results.ValidationResult(validationFailures));

            // Act
            var result = await _validationBehavior.Handle(createStudioRequest, _mockNextBehavior, default);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be("property");
            result.FirstError.Description.Should().Be("property error message");
        }
    }
}
