using DanceStudio.Domain.Subscriptions;
using DanceStudio.TestCommon.Studios;
using DanceStudio.TestCommon.Subscriptions;
using ErrorOr;
using FluentAssertions;

namespace DanceStudio.DomainUnitTest.Subscriptions
{
    public class SubscriptionTests
    {
        [Fact]
        public void AddStudio_WithMoreThanSubscriptionsIsAllowed_ShouldFail()
        {
            //Arrange
            //Create a subscription
            var subscription = SubscriptionFactory.CreateSubscription();

            //Create the max number of studios + 1
            var studios = Enumerable.Range(0, subscription.GetMaxStudios() + 1)
                .Select(_ => StudioFactory.CreateStudio(id: Guid.NewGuid()))
                .ToList();

            //Act
            //Add all the various studios
            var addStudioResults = studios.ConvertAll(subscription.AddStudio);

            //Assert
            //Adding all the studios is success but the last fails
            var allButLastStudioResults = addStudioResults[..^1];
            allButLastStudioResults.Should().AllSatisfy(addGymResult => addGymResult.Value.Should().Be(Result.Success));

            var lastAddStudioResult = addStudioResults.Last();
            lastAddStudioResult.IsError.Should().BeTrue();
            lastAddStudioResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreStudiosThanTheSubscriptionAllows);

        }
    }
}
