FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 82
EXPOSE 445

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ECommerceTest.CartApi/ECommerceTest.CartApi.csproj", "ECommerceTest.CartApi/"]
COPY ["ECommerceTest.ApiGateway/ECommerceTest.ApiGateway.csproj", "ECommerceTest.ApiGateway/"]
COPY ["ECommerceTest.Models/ECommerceTest.Models.csproj", "ECommerceTest.Models/"]
RUN dotnet restore "ECommerceTest.CartApi/ECommerceTest.CartApi.csproj"
COPY . .
WORKDIR "/src/ECommerceTest.CartApi"
RUN dotnet build "ECommerceTest.CartApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerceTest.CartApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceTest.CartApi.dll"]