# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY RestApi/RestApi.csproj RestApi/
RUN dotnet restore RestApi/RestApi.csproj

# Copy the rest of the code
COPY . .

# Build the application
WORKDIR /src/RestApi
RUN dotnet publish -c Release -o /app

# Set the base image to the .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app .

# Expose the port for the REST API service
EXPOSE 5046

# Set the entry point for the REST API service
ENTRYPOINT ["dotnet", "RestApi.dll"]
