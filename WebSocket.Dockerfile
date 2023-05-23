FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy and restore WebSocket project
COPY ["WebSocket/WebSocket.csproj", "WebSocket/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["WSDAOs/WSDAOs.csproj", "WSDAOs/"]
RUN dotnet restore "WebSocket/WebSocket.csproj"

# Copy the solution and build
COPY . .
WORKDIR "/src/WebSocket"
RUN dotnet build "WebSocket.csproj" -c Release -o /app/build

# Publish WebSocket project
FROM build AS websocket-publish
WORKDIR "/src/WebSocket"
RUN dotnet publish "WebSocket.csproj" -c Release -o /app/publish/WebSocket /p:UseAppHost=false

# Create the final image and copy published outputs
FROM base AS websocket-final
WORKDIR /app
COPY --from=websocket-publish /app/publish/WebSocket .

# Set the entry point and command for WebSocket
ENTRYPOINT ["dotnet", "WebSocket.dll"]
