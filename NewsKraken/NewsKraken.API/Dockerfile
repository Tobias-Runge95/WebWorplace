FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RabbitRequestModels/RabbitRequestModels.csproj", "RabbitRequestModels/"]
COPY ["NewsKraken/NewsKraken.API/NewsKraken.API.csproj", "NewsKraken/NewsKraken.API/"]
COPY ["NewsKraken/NewsKraken.Core/NewsKraken.Core.csproj", "NewsKraken/NewsKraken.Core/"]
COPY ["NewsKraken/NewsKraken.Database/NewsKraken.Database.csproj", "NewsKraken/NewsKraken.Database/"]
RUN dotnet restore "NewsKraken/NewsKraken.API/NewsKraken.API.csproj"
COPY . .
WORKDIR /src/NewsKraken/NewsKraken.API
RUN dotnet build "NewsKraken.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsKraken.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsKraken.API.dll"]
