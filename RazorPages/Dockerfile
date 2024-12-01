FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy the project file and restore dependencies
COPY RazorPages/RazorPages.csproj RazorPages/
RUN dotnet restore RazorPages/RazorPages.csproj

# Copy the rest of the code
COPY . .

# Build and publish the app
WORKDIR /src/RazorPages
RUN dotnet publish -c Release -o /app/razorpages

# Set the base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/razorpages /app
EXPOSE 5000
ENTRYPOINT ["dotnet", "RazorPages.dll"]
