#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InPostApp/Server/InPostApp.Server.csproj", "InPostApp/Server/"]
COPY ["InPostApp/Client/InPostApp.Client.csproj", "InPostApp/Client/"]
COPY ["InPostApp/Shared/InPostApp.Shared.csproj", "InPostApp/Shared/"]
RUN dotnet restore "InPostApp/Server/InPostApp.Server.csproj"
COPY . .
WORKDIR "/src/InPostApp/Server"
RUN dotnet build "InPostApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InPostApp.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InPostApp.Server.dll"]