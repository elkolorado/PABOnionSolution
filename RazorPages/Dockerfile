#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
RUN dotnet dev-certs https
WORKDIR /src
COPY ["RazorPages/RazorPages.csproj", "RazorPages/"]
COPY ["RestApi/RestApi.csproj", "RestApi/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["SoapService/SoapService.csproj", "SoapService/"]
RUN dotnet restore "./RazorPages/./RazorPages.csproj"
COPY . .
WORKDIR "/src/RazorPages"
RUN dotnet build "./RazorPages.csproj" -c $BUILD_CONFIGURATION -o /app/build/RazorPages

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RazorPages.csproj" -c $BUILD_CONFIGURATION -o /app/publish/RazorPages /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish/RazorPages .
ENTRYPOINT ["dotnet", "RazorPages.dll"]