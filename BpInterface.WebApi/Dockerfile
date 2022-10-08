#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BpInterface.WebApi/BpInterface.WebApi.csproj", "BpInterface.WebApi/"]
COPY ["BpInterface.Core/BpInterface.Core.csproj", "BpInterface.Core/"]
COPY ["BpInterface.Infrastructure/BpInterface.Infrastructure.csproj", "BpInterface.Infrastructure/"]
COPY ["BpInterface.Services/BpInterface.Services.csproj", "BpInterface.Services/"]
RUN dotnet restore "BpInterface.WebApi/BpInterface.WebApi.csproj"
COPY . .
WORKDIR "/src/BpInterface.WebApi"
RUN dotnet build "BpInterface.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BpInterface.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BpInterface.WebApi.dll"]