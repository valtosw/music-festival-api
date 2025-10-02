FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MusicFestival.API.csproj", "Festival.API/"]
COPY ["../Festival.Core/MusicFestival.Core.csproj", "Festival.Core/"]
COPY ["../Festival.Data/MusicFestival.Data.csproj", "Festival.Data/"]

RUN dotnet restore "Festival.API/MusicFestival.API.csproj"

COPY ../ .


WORKDIR "/src/Festival.API"
RUN dotnet publish "Festival.API.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Festival.API.dll"]