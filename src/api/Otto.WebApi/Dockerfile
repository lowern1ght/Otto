FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/api/Otto.WebApi/Otto.WebApi.csproj", "src/api/Otto.WebApi/"]
RUN dotnet restore "src/api/Otto.WebApi/Otto.WebApi.csproj"
COPY . .
WORKDIR "/src/src/api/Otto.WebApi"
RUN dotnet build "Otto.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Otto.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Otto.WebApi.dll"]
