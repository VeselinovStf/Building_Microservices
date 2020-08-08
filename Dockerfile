FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /src/JustOrganize.TeamService

COPY /src/JustOrganize.TeamService/*.csproj ./
RUN dotnet restore

COPY /src/JustOrganize.TeamService/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /src/JustOrganize.TeamService
COPY --from=build-env /src/JustOrganize.TeamService .
ENTRYPOINT ["dotnet", "JustOrganize.TeamService.dll"]