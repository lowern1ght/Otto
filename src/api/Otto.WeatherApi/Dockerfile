FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/api/Otto.WeatherApi/Otto.WeatherApi.csproj", "src/api/Otto.WeatherApi/"]
COPY ["src/services/Otto.WeatherService/Otto.WeatherService.csproj", "src/services/Otto.WeatherService/"]
RUN dotnet restore "src/api/Otto.WeatherApi/Otto.WeatherApi.csproj"
COPY . .
WORKDIR "/src/src/api/Otto.WeatherApi"

RUN dotnet build "Otto.WeatherApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Otto.WeatherApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Otto.WeatherApi.dll"]
