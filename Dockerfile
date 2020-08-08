FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /src

COPY /src/JustOrganize.TeamService/*.csproj ./
RUN dotnet restore

COPY /src/JustOrganize.TeamService ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /src
COPY --from=build /src/out .
ENTRYPOINT ["dotnet", "JustOrganize.TeamService.dll"]