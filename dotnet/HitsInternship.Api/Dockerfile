# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT=Development


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["dotnet/HitsInternship.Api/HitsInternship.Api.csproj", "HitsInternship.Api/"]
COPY ["dotnet/AppSettingsModule.Controllers/AppSettingsModule.Controllers.csproj", "AppSettingsModule.Controllers/"]
COPY ["dotnet/AppSettingsModule.Application/AppSettingsModule.Application.csproj", "AppSettingsModule.Application/"]
COPY ["dotnet/AppSettingsModule.Contracts/AppSettingsModule.Contracts.csproj", "AppSettingsModule.Contracts/"]
COPY ["dotnet/AppSettingsModule.Domain/AppSettingsModule.Domain.csproj", "AppSettingsModule.Domain/"]
COPY ["dotnet/Shared.Domain/Shared.Domain.csproj", "Shared.Domain/"]
COPY ["dotnet/Shared.Contracts/Shared.Contracts.csproj", "Shared.Contracts/"]
COPY ["dotnet/AppSettingsModule.Infrastructure/AppSettingsModule.Infrastructure.csproj", "AppSettingsModule.Infrastructure/"]
COPY ["dotnet/AppSettingsModule.Persistence/AppSettingsModule.Persistence.csproj", "AppSettingsModule.Persistence/"]
COPY ["dotnet/Shared.Persistence/Shared.Persistence.csproj", "Shared.Persistence/"]
COPY ["dotnet/AuthModule.Application/AuthModule.Application.csproj", "AuthModule.Application/"]
COPY ["dotnet/AuthModule.Infrastructure/AuthModule.Infrastructure.csproj", "AuthModule.Infrastructure/"]
COPY ["dotnet/AuthModule.Domain/AuthModule.Domain.csproj", "AuthModule.Domain/"]
COPY ["dotnet/UserModule.Domain/UserModule.Domain.csproj", "UserModule.Domain/"]
COPY ["dotnet/AuthModule.Contracts/AuthModule.Contracts.csproj", "AuthModule.Contracts/"]
COPY ["dotnet/Shared.Extensions/Shared.Extensions.csproj", "Shared.Extensions/"]
COPY ["dotnet/UserModule.Contracts/UserModule.Contracts.csproj", "UserModule.Contracts/"]
COPY ["dotnet/AuthModule.Controlllers/AuthModule.Controlllers.csproj", "AuthModule.Controlllers/"]
COPY ["dotnet/CompanyModule.Contracts/CompanyModule.Contracts.csproj", "CompanyModule.Contracts/"]
COPY ["dotnet/CompanyModule.Domain/CompanyModule.Domain.csproj", "CompanyModule.Domain/"]
COPY ["dotnet/CompanyModule.Controllers/CompanyModule.Controllers.csproj", "CompanyModule.Controllers/"]
COPY ["dotnet/CompanyModule.Application/CompanyModule.Application.csproj", "CompanyModule.Application/"]
COPY ["dotnet/DocumentModule.Contracts/DocumentModule.Contracts.csproj", "DocumentModule.Contracts/"]
COPY ["dotnet/DocumentModule.Domain/DocumentModule.Domain.csproj", "DocumentModule.Domain/"]
COPY ["dotnet/CompanyModule.Infrastructure/CompanyModule.Infrastructure.csproj", "CompanyModule.Infrastructure/"]
COPY ["dotnet/CompanyModule.Persistence/CompanyModule.Persistence.csproj", "CompanyModule.Persistence/"]
COPY ["dotnet/DeanModule.Controllers/DeanModule.Controllers.csproj", "DeanModule.Controllers/"]
COPY ["dotnet/DeanModule.Application/DeanModule.Application.csproj", "DeanModule.Application/"]
COPY ["dotnet/DeanModule.Contracts/DeanModule.Contracts.csproj", "DeanModule.Contracts/"]
COPY ["dotnet/DeanModule.Domain/DeanModule.Domain.csproj", "DeanModule.Domain/"]
COPY ["dotnet/SelectionModule.Contracts/SelectionModule.Contracts.csproj", "SelectionModule.Contracts/"]
COPY ["dotnet/SelectionModule.Domain/SelectionModule.Domain.csproj", "SelectionModule.Domain/"]
COPY ["dotnet/StudentModule.Contracts/StudentModule.Contracts.csproj", "StudentModule.Contracts/"]
COPY ["dotnet/StudentModule.Domain/StudentModule.Domain.csproj", "StudentModule.Domain/"]
COPY ["dotnet/DeanModule.Infrastructure/DeanModule.Infrastructure.csproj", "DeanModule.Infrastructure/"]
COPY ["dotnet/DeanModule.Persistence/DeanModule.Persistence.csproj", "DeanModule.Persistence/"]
COPY ["dotnet/UserModule.Persistence/UserModule.Persistence.csproj", "UserModule.Persistence/"]
COPY ["dotnet/UserModule.Infrastructure/UserModule.Infrastructure.csproj", "UserModule.Infrastructure/"]
COPY ["dotnet/DocumentModule.Controllers/DocumentModule.Controllers.csproj", "DocumentModule.Controllers/"]
COPY ["dotnet/DocumentModule.Application/DocumentModule.Application.csproj", "DocumentModule.Application/"]
COPY ["dotnet/DocumentModule.Persistence/DocumentModule.Persistence.csproj", "DocumentModule.Persistence/"]
COPY ["dotnet/DocumentModule.Infrastructure/DocumentModule.Infrastructure.csproj", "DocumentModule.Infrastructure/"]
COPY ["dotnet/SelectionModule.Controllers/SelectionModule.Controllers.csproj", "SelectionModule.Controllers/"]
COPY ["dotnet/SelectionModule.Application/SelectionModule.Application.csproj", "SelectionModule.Application/"]
COPY ["dotnet/SelectionModule.Infrastructure/SelectionModule.Infrastructure.csproj", "SelectionModule.Infrastructure/"]
COPY ["dotnet/SelectionModule.Persistence/SelectionModule.Persistence.csproj", "SelectionModule.Persistence/"]
COPY ["dotnet/PracticeModule.Controllers/PracticeModule.Controllers.csproj", "PracticeModule.Controllers/"]
COPY ["dotnet/PracticeModule.Application/PracticeModule.Application.csproj", "PracticeModule.Application/"]
COPY ["dotnet/PracticeModule.Contracts/PracticeModule.Contracts.csproj", "PracticeModule.Contracts/"]
COPY ["dotnet/PracticeModule.Domain/PracticeModule.Domain.csproj", "PracticeModule.Domain/"]
COPY ["dotnet/PracticeModule.Infrastructure/PracticeModule.Infrastructure.csproj", "PracticeModule.Infrastructure/"]
COPY ["dotnet/StudentModule.Controllers/StudentModule.Controllers.csproj", "StudentModule.Controllers/"]
COPY ["dotnet/StudentModule.Application/StudentModule.Application.csproj", "StudentModule.Application/"]
COPY ["dotnet/StudentModule.Infrastructure/StudentModule.Infrastructure.csproj", "StudentModule.Infrastructure/"]
COPY ["dotnet/StudentModule.Persistence/StudentModule.Persistence.csproj", "StudentModule.Persistence/"]
COPY ["dotnet/UserModule.Controllers/UserModule.Controllers.csproj", "UserModule.Controllers/"]
COPY ["dotnet/UserModule.Application/UserModule.Application.csproj", "UserModule.Application/"]
RUN dotnet restore "./HitsInternship.Api/HitsInternship.Api.csproj"
COPY . .
WORKDIR "/src/dotnet/HitsInternship.Api"
RUN dotnet build "./HitsInternship.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HitsInternship.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HitsInternship.Api.dll"]