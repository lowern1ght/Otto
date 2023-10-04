FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

ARG configuration=Release

WORKDIR ./src
COPY ["src/api/Otto.CityApi/Otto.CityApi.csproj", "src/api/Otto.CityApi/"]

RUN dotnet restore "src/api/Otto.CityApi/Otto.CityApi.csproj"

COPY . .

WORKDIR "/src/api/Otto.CityApi"
RUN dotnet build "Otto.CityApi.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release

RUN dotnet publish "Otto.CityApi.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Otto.CityApi.dll"]
