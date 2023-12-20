FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestEFLayer/TestEFLayer.csproj", "TestEFLayer/"]
RUN dotnet restore "TestEFLayer/TestEFLayer.csproj"
COPY . .
WORKDIR "/src/TestEFLayer"
RUN dotnet build "TestEFLayer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestEFLayer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestEFLayer.dll"]
