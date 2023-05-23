FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy and restore RestAPI project
COPY ["RestAPI/RestAPI.csproj", "RestAPI/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["RestDAOs/RestDAOs.csproj", "RestDAOs/"]
RUN dotnet restore "RestAPI/RestAPI.csproj"

# Copy and restore WebSocket project
COPY ["WebSocket/WebSocket.csproj", "WebSocket/"]
COPY ["WSDAOs/WSDAOs.csproj", "WSDAOs/"]
RUN dotnet restore "WebSocket/WebSocket.csproj"

# Copy the solution and build
COPY . .
WORKDIR "/src/RestAPI"
RUN dotnet build "RestAPI.csproj" -c Release -o /app/build

WORKDIR "/src/WebSocket"
RUN dotnet build "WebSocket.csproj" -c Release -o /app/build

# Publish RestAPI project
FROM build AS restapi-publish
WORKDIR "/src/RestAPI"
RUN dotnet publish "RestAPI.csproj" -c Release -o /app/publish/RestAPI /p:UseAppHost=false

# Publish WebSocket project
FROM build AS websocket-publish
WORKDIR "/src/WebSocket"
RUN dotnet publish "WebSocket.csproj" -c Release -o /app/publish/WebSocket /p:UseAppHost=false

# Create the final image and copy published outputs
FROM base AS final
WORKDIR /app
COPY --from=restapi-publish /app/publish/RestAPI .
COPY --from=websocket-publish /app/publish/WebSocket .

# Set the entry point and command for RestAPI
ENTRYPOINT ["dotnet", "RestAPI.dll"]

# Start the WebSocket project in the background
CMD ["sh", "-c", "cd /app/publish/WebSocket && dotnet WebSocket.dll"]