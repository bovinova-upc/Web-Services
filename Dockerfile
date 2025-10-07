FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY VacApp-Bovinova-Platform/*.csproj ./VacApp-Bovinova-Platform/
WORKDIR /app/VacApp-Bovinova-Platform
RUN dotnet restore

COPY VacApp-Bovinova-Platform/. ./

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish ./

EXPOSE 80

ENTRYPOINT ["dotnet", "VacApp-Bovinova-Platform.dll"]