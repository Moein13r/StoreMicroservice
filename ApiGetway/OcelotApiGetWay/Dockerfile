#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OcelotApiGetWay/OcelotApiGetWay.csproj", "OcelotApiGetWay/"]
RUN dotnet restore "OcelotApiGetWay/OcelotApiGetWay.csproj"
COPY . .
WORKDIR "/src/OcelotApiGetWay"
RUN dotnet build "OcelotApiGetWay.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcelotApiGetWay.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotApiGetWay.dll"]