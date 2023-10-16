#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["fake_app.csproj", "."]
RUN dotnet restore "./fake_app.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "fake_app.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fake_app.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fake_app.dll", "20"]