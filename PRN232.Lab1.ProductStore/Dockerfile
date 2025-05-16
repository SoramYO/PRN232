FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution và tất cả các project
COPY ["PRN232.Lab1.ProductStore.API/PRN232.Lab1.ProductStore.API.csproj", "PRN232.Lab1.ProductStore.API/"]
COPY ["PRN232.Lab1.ProductStore.Repository/PRN232.Lab1.ProductStore.Repository.csproj", "PRN232.Lab1.ProductStore.Repository/"]
COPY ["PRN232.Lab1.ProductStore.Service/PRN232.Lab1.ProductStore.Service.csproj", "PRN232.Lab1.ProductStore.Service/"]

# Restore packages
RUN dotnet restore "PRN232.Lab1.ProductStore.API/PRN232.Lab1.ProductStore.API.csproj"

# Copy tất cả source code
COPY . .

# Build project
WORKDIR "/src/PRN232.Lab1.ProductStore.API"
RUN dotnet build "PRN232.Lab1.ProductStore.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "PRN232.Lab1.ProductStore.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PRN232.Lab1.ProductStore.API.dll"]