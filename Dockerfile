FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["YandexCup22.csproj", "./"]
RUN dotnet restore "YandexCup22.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "YandexCup22.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YandexCup22.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YandexCup22.dll"]
