FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /var/www/ReviveIt/ReviveIT

# Copy the solution file and all referenced project files
COPY ReviveIT.sln ./
COPY Application/Application.csproj ./Application/
COPY Domain/Domain.csproj ./Domain/
COPY Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY ReviveIt.test/ReviveIt.test.csproj ./ReviveIt.test/
COPY WebUI/WebUI.csproj ./WebUI/

# Restore dependencies
RUN dotnet restore

# Copy the entire project
COPY . .

# Build the project
WORKDIR /var/www/ReviveIt/ReviveIT/WebUI
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]

