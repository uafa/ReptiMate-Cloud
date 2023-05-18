FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RestAPI/RestAPI.csproj", "RestAPI/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["WebSocket/WebSocket.csproj", "WebSocket/"]
COPY ["WSDAOs/WSDAOs.csproj", "WSDAOs/"]
COPY ["RestDAOs/RestDAOs.csproj", "RestDAOs/"]
RUN dotnet restore "RestAPI/RestAPI.csproj"
COPY . .
WORKDIR "/src/RestAPI"

RUN dotnet build "RestAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build the WebSocket project
FROM build AS build-websocket
WORKDIR /src/WebSocket
RUN dotnet build "WebSocket.csproj" -c Release -o /app/build

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the WebSocket project output to the final container
COPY --from=build-websocket /app/build /app/WebSocket

# Set the entrypoint for the container
ENTRYPOINT ["dotnet", "RestAPI.dll"]
