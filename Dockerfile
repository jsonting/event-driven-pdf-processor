# ==========================================
# Base Stage
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ==========================================
# Build Stage
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["pdfProcessor.csproj", "./"]
RUN dotnet restore "pdfProcessor.csproj"

COPY . .
WORKDIR "/src/"

RUN dotnet publish "./pdfProcessor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ==========================================
# Final Stage
# ==========================================
FROM base AS final
WORKDIR /app

#Switch to non-root user AFTER copying files, OR use --chown so the non-root user owns the app files.
COPY --from=build --chown=$APP_UID:$APP_UID /app/publish .

# Enforce non-root execution
USER $APP_UID

ENTRYPOINT ["dotnet", "pdfProcessor.dll"]