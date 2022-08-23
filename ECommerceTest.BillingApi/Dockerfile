FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 81
EXPOSE 444

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ECommerceTest.BillingApi/ECommerceTest.BillingApi.csproj", "ECommerceTest.BillingApi/"]
RUN dotnet restore "ECommerceTest.BillingApi/ECommerceTest.BillingApi.csproj"
COPY . .
WORKDIR "/src/ECommerceTest.BillingApi"
RUN dotnet build "ECommerceTest.BillingApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerceTest.BillingApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceTest.BillingApi.dll"]