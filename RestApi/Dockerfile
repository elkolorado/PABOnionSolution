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
COPY ["RestApi/RestApi.csproj", "RestApi/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "./RestApi/./RestApi.csproj"
COPY . .
WORKDIR "/src/RestApi"
RUN dotnet build "./RestApi.csproj" -c $BUILD_CONFIGURATION -o /app/build/RestApi

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RestApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish/RestApi /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish/RestApi .
ENTRYPOINT ["dotnet", "RestApi.dll"]