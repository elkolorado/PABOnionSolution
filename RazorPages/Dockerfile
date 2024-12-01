# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY RazorPages/RazorPages.csproj RazorPages/
RUN dotnet restore RazorPages/RazorPages.csproj

# Copy the rest of the code
COPY . .

# Build the application
WORKDIR /src/RazorPages
RUN dotnet publish -c Release -o /app

# Set the base image to the .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app .

# Expose the port for the Razor Pages app
EXPOSE 5000

# Set the entry point for Razor Pages
ENTRYPOINT ["dotnet", "RazorPages.dll"]
