FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 84
EXPOSE 447

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ECommerceTest.ApiGateway/ECommerceTest.ApiGateway.csproj", "ECommerceTest.ApiGateway/"]
COPY ["ECommerceTest.Models/ECommerceTest.Models.csproj", "ECommerceTest.Models/"]
RUN dotnet restore "ECommerceTest.ApiGateway/ECommerceTest.ApiGateway.csproj"
COPY . .
WORKDIR "/src/ECommerceTest.ApiGateway"
RUN dotnet build "ECommerceTest.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerceTest.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceTest.ApiGateway.dll"]