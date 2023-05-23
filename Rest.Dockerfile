FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy and restore RestAPI project
COPY ["RestAPI/RestAPI.csproj", "RestAPI/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["RestDAOs/RestDAOs.csproj", "RestDAOs/"]
RUN dotnet restore "RestAPI/RestAPI.csproj"

# Copy the solution and build
COPY . .
WORKDIR "/src/RestAPI"
RUN dotnet build "RestAPI.csproj" -c Release -o /app/build

# Publish RestAPI project
FROM build AS restapi-publish
WORKDIR "/src/RestAPI"
RUN dotnet publish "RestAPI.csproj" -c Release -o /app/publish/RestAPI /p:UseAppHost=false

# Create the final image and copy published outputs
FROM base AS restapi-final
WORKDIR /app
COPY --from=restapi-publish /app/publish/RestAPI .

# Set the entry point and command for RestAPI
ENTRYPOINT ["dotnet", "RestAPI.dll"]
