FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebWorkPlace.API/WebWorkPlace.API.csproj", "WebWorkPlace.API/"]
COPY ["WebWorkPlace.Core/WebWorkPlace.Core.csproj", "WebWorkPlace.Core/"]
COPY ["HelperLibrary/HelperLibrary.csproj", "HelperLibrary/"]
RUN dotnet restore "WebWorkPlace.API/WebWorkPlace.API.csproj"
COPY . .
WORKDIR "/src/WebWorkPlace.API"
RUN dotnet build "WebWorkPlace.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebWorkPlace.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebWorkPlace.API.dll"]
