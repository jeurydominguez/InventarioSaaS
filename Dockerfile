# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./

RUN dotnet restore InventarioSaaS.API/InventarioSaaS.API.csproj
RUN dotnet publish InventarioSaaS.API/InventarioSaaS.API.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

CMD ["dotnet", "InventarioSaaS.API.dll"]