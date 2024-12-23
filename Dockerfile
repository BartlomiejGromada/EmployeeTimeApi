# Etap 1: Budowanie test�w jednostkowych
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-tests-unit
WORKDIR /src
COPY . .
RUN dotnet restore "tests/EmployeeTimeApi.Tests.Unit/EmployeeTimeApi.Tests.Unit.csproj"
RUN dotnet test "tests/EmployeeTimeApi.Tests.Unit/EmployeeTimeApi.Tests.Unit.csproj" --no-restore --verbosity normal

# Etap 2: Budowanie test�w integracyjnych
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-tests-integration
WORKDIR /src
COPY . .
RUN dotnet restore "tests/EmployeeTimeApi.Tests.Integration/EmployeeTimeApi.Tests.Integration.csproj"
RUN dotnet test "tests/EmployeeTimeApi.Tests.Integration/EmployeeTimeApi.Tests.Integration.csproj" --no-restore --verbosity normal

# Etap 3: Budowanie aplikacji
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/EmployeeTimeApi.Shared.Infrastructure/EmployeeTimeApi.Shared.Infrastructure.csproj", "EmployeeTimeApi.Shared.Infrastructure/"]

COPY ["src/EmployeeTimeApi.Shared.Abstractions/EmployeeTimeApi.Shared.Abstractions.csproj", "EmployeeTimeApi.Shared.Abstractions/"]

COPY ["src/EmployeeTimeApi.Application/EmployeeTimeApi.Application.csproj", "EmployeeTimeApi.Application/"]

COPY ["src/EmployeeTimeApi.Domain/EmployeeTimeApi.Domain.csproj", "EmployeeTimeApi.Domain/"]

COPY ["src/EmployeeTimeApi.Infrastructure/EmployeeTimeApi.Infrastructure.csproj", "EmployeeTimeApi.Infrastructure/"]

COPY ["src/EmployeeTimeApi.Presentation/EmployeeTimeApi.Presentation.csproj", "EmployeeTimeApi.Presentation/"]

RUN dotnet restore "EmployeeTimeApi.Presentation/EmployeeTimeApi.Presentation.csproj"

COPY . ../

WORKDIR /src/EmployeeTimeApi.Presentation

RUN dotnet build "EmployeeTimeApi.Presentation.csproj" --no-restore -c Release -o /app/publish

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

# Etap 4: Obraz runtime dla aplikacji
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeTimeApi.Presentation.dll"]
