# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy entire repo for simplicity (includes all .csproj and solution)
COPY . .

# Restore solution (caches nuget packages)
RUN dotnet restore Doara.sln

# Publish migrator
RUN dotnet publish src/Doara.DbMigrator/Doara.DbMigrator.csproj -c Release -o /app/migrator

# Publish host
RUN dotnet publish src/Doara.HttpApi.Host/Doara.HttpApi.Host.csproj -c Release -o /app/host

# 2. Migrator runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS migrator
WORKDIR /app
COPY --from=build /app/migrator .
ENTRYPOINT ["dotnet", "Doara.DbMigrator.dll"]

# 3. Host runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS host
WORKDIR /app
COPY --from=build /app/host .

# Listen on port 80
ENV ASPNETCORE_URLS=http://+:80
# Fallback connection string if compose env not applied
ENV ConnectionStrings__Default="Server=doara-db,1433;Database=Api;User=sa;Password=D0ara!sBest;MultipleActiveResultSets=true;TrustServerCertificate=True;"
EXPOSE 80
ENTRYPOINT ["dotnet", "Doara.HttpApi.Host.dll"]