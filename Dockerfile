FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["DanceStudio.sln", "./"]
COPY ["src/DanceStudio.Api/DanceStudio.Api.csproj", "src/DanceStudio.Api/"]
COPY ["src/DanceStudio.Application/DanceStudio.Application.csproj", "src/DanceStudio.Application/"]
COPY ["src/DanceStudio.Contracts/DanceStudio.Contracts.csproj", "src/DanceStudio.Contracts/"]
COPY ["src/DanceStudio.Domain/DanceStudio.Domain.csproj", "src/DanceStudio.Domain/"]
COPY ["src/DanceStudio.Infrastructure/DanceStudio.Infrastructure.csproj", "src/DanceStudio.Infrastructure/"]

RUN dotnet restore "src/DanceStudio.Api/DanceStudio.Api.csproj"

COPY src ./src

RUN dotnet publish "src/DanceStudio.Api/DanceStudio.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DanceStudio.Api.dll"]

