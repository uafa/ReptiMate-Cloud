FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RestAPI/RestAPI.csproj", "RestAPI/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["WebSocket/WebSocket.csproj", "WebSocket/"]
COPY ["WSDAOs/WSDAOs.csproj", "WSDAOs/"]
COPY ["RestDAOs/RestDAOs.csproj", "RestDAOs/"]
RUN dotnet restore "RestAPI/RestAPI.csproj"
RUN dotnet restore "WebSocket/WebSocket.csproj"
COPY . .
WORKDIR "/src/RestAPI"

RUN dotnet build "RestAPI.csproj" -c Release -o /app/build

WORKDIR "/src/WebSocket"

RUN dotnet build "WebSocket.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/RestAPI"
RUN dotnet publish "RestAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

WORKDIR "/src/WebSocket"
RUN dotnet publish "WebSocket.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the entrypoint for the container
ENTRYPOINT ["dotnet", "RestAPI.dll", "--urls", "http://0.0.0.0:80", "https://0.0.0.0:443"]
