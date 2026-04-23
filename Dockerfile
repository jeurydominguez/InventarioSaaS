# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar todo
COPY . ./

# Restaurar SOLO el proyecto API
RUN dotnet restore InventarioSaaS.API/InventarioSaaS.API.csproj

# Publicar la API
RUN dotnet publish InventarioSaaS.API/InventarioSaaS.API.csproj -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Puerto Railway
ENV ASPNETCORE_URLS=http://+:${PORT}

# Ejecutar
ENTRYPOINT ["dotnet", "InventarioSaaS.API.dll"]