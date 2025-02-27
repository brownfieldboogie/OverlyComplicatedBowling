#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OverlyComplicatedBowling.Infrastructure.Migrations/OverlyComplicatedBowling.Infrastructure.Migrations.csproj", "src/OverlyComplicatedBowling.Infrastructure.Migrations/"]
COPY ["src/OverlyComplicatedBowling.Domain/OverlyComplicatedBowling.Domain.csproj", "src/OverlyComplicatedBowling.Domain/"]
COPY ["src/OverlyComplicatedBowling.Infrastructure/OverlyComplicatedBowling.Infrastructure.csproj", "src/OverlyComplicatedBowling.Infrastructure/"]
COPY ["src/OverlyComplicatedBowling.Application/OverlyComplicatedBowling.Application.csproj", "src/OverlyComplicatedBowling.Application/"]
RUN dotnet restore "./src/OverlyComplicatedBowling.Infrastructure.Migrations/OverlyComplicatedBowling.Infrastructure.Migrations.csproj"
COPY . .
WORKDIR "/src/src/OverlyComplicatedBowling.Infrastructure.Migrations"
RUN dotnet build "./OverlyComplicatedBowling.Infrastructure.Migrations.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OverlyComplicatedBowling.Infrastructure.Migrations.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OverlyComplicatedBowling.Infrastructure.Migrations.dll"]