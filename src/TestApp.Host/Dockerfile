FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Directory.Build.targets", "."]
COPY ["Directory.Packages.props", "."]
COPY ["TestApp.Host/TestApp.Host.csproj", "TestApp.Host/"]
RUN dotnet restore "./TestApp.Host/TestApp.Host.csproj"
COPY . .
WORKDIR "/src/TestApp.Host"
RUN dotnet build "./TestApp.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TestApp.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestApp.Host.dll"]