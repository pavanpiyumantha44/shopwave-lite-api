# ── Stage 1: Build ─────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files first (better layer caching)
COPY ShopWaveLite.sln ./
COPY ShopWaveLite.Api/ShopWaveLite.Api.csproj ShopWaveLite.Api/

# Restore dependencies
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /src/ShopWaveLite.Api
RUN dotnet publish -c Release -o /app/publish

# ── Stage 2: Runtime ───────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Render assigns the port via $PORT env variable
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}

ENTRYPOINT ["dotnet", "ShopWaveLite.Api.dll"]