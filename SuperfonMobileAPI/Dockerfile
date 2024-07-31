FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SuperfonMobileAPI/SuperfonMobileAPI.csproj", "SuperfonMobileAPI/"]
RUN dotnet restore "SuperfonMobileAPI/SuperfonMobileAPI.csproj"
COPY . .
WORKDIR "/src/SuperfonMobileAPI"
RUN dotnet build "SuperfonMobileAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SuperfonMobileAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperfonMobileAPI.dll"]
