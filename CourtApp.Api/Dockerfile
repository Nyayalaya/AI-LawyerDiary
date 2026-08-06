# ================================
# Build Stage
# ================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy the complete solution
COPY . .

# Restore API project
RUN dotnet restore "CourtApp.Api/CourtApp.Api.csproj"

# Publish
RUN dotnet publish "CourtApp.Api/CourtApp.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

# ================================
# Runtime Stage
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000

EXPOSE 10000

ENTRYPOINT ["dotnet","CourtApp.Api.dll"]