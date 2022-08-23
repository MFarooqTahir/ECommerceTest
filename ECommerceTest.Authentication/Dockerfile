FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ECommerceTest.Authentication/ECommerceTest.Authentication.csproj", "ECommerceTest.Authentication/"]
RUN dotnet restore "ECommerceTest.Authentication/ECommerceTest.Authentication.csproj"
COPY . .
WORKDIR "/src/ECommerceTest.Authentication"
RUN dotnet build "ECommerceTest.Authentication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerceTest.Authentication.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceTest.Authentication.dll"]