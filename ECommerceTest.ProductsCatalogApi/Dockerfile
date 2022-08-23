FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 83
EXPOSE 446

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ECommerceTest.ProductsCatalogApi/ECommerceTest.ProductsCatalogApi.csproj", "ECommerceTest.ProductsCatalogApi/"]
RUN dotnet restore "ECommerceTest.ProductsCatalogApi/ECommerceTest.ProductsCatalogApi.csproj"
COPY . .
WORKDIR "/src/ECommerceTest.ProductsCatalogApi"
RUN dotnet build "ECommerceTest.ProductsCatalogApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerceTest.ProductsCatalogApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceTest.ProductsCatalogApi.dll"]