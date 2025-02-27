#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OverlyComplicatedBowling.Presentation.Api/OverlyComplicatedBowling.Presentation.Api.csproj", "src/OverlyComplicatedBowling.Presentation.Api/"]
COPY ["src/OverlyComplicatedBowling.Application/OverlyComplicatedBowling.Application.csproj", "src/OverlyComplicatedBowling.Application/"]
COPY ["src/OverlyComplicatedBowling.Domain/OverlyComplicatedBowling.Domain.csproj", "src/OverlyComplicatedBowling.Domain/"]
COPY ["src/OverlyComplicatedBowling.Infrastructure/OverlyComplicatedBowling.Infrastructure.csproj", "src/OverlyComplicatedBowling.Infrastructure/"]
RUN dotnet restore "./src/OverlyComplicatedBowling.Presentation.Api/OverlyComplicatedBowling.Presentation.Api.csproj"
COPY . .
WORKDIR "/src/src/OverlyComplicatedBowling.Presentation.Api"
RUN dotnet build "./OverlyComplicatedBowling.Presentation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OverlyComplicatedBowling.Presentation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OverlyComplicatedBowling.Presentation.Api.dll"]