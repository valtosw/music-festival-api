FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MusicFestival.API.csproj", "MusicFestival.API/"]
COPY ["../MusicFestival.Core/MusicFestival.Core.csproj", "MusicFestival.Core/"]
COPY ["../MusicFestival.Data/MusicFestival.Data.csproj", "MusicFestival.Data/"]

RUN dotnet restore "MusicFestival.API/MusicFestival.API.csproj"

COPY ../ .

WORKDIR "/src/MusicFestival.API"
RUN dotnet publish "MusicFestival.API.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MusicFestival.API.dll"]