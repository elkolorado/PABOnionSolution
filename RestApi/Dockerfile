FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy the project file and restore dependencies
COPY RestApi/RestApi.csproj RestApi/
RUN dotnet restore RestApi/RestApi.csproj

# Copy the rest of the code
COPY . .

# Build and publish the app
WORKDIR /src/RestApi
RUN dotnet publish -c Release -o /app

# Set the base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 5046
ENTRYPOINT ["dotnet", "RestApi.dll"]
