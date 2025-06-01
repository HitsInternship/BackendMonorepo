FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/dotnet
COPY ["HitsInternship.Api/HitsInternship.Api.csproj", "HitsInternship.Api/"]
COPY ["AppSettingsModule.Controllers/AppSettingsModule.Controllers.csproj", "AppSettingsModule.Controllers/"]
COPY ["AppSettingsModule.Application/AppSettingsModule.Application.csproj", "AppSettingsModule.Application/"]
COPY ["AppSettingsModule.Contracts/AppSettingsModule.Contracts.csproj", "AppSettingsModule.Contracts/"]
COPY ["AppSettingsModule.Domain/AppSettingsModule.Domain.csproj", "AppSettingsModule.Domain/"]
COPY ["Shared.Domain/Shared.Domain.csproj", "Shared.Domain/"]
COPY ["Shared.Contracts/Shared.Contracts.csproj", "Shared.Contracts/"]
COPY ["AppSettingsModule.Infrastructure/AppSettingsModule.Infrastructure.csproj", "AppSettingsModule.Infrastructure/"]
COPY ["AppSettingsModule.Persistence/AppSettingsModule.Persistence.csproj", "AppSettingsModule.Persistence/"]
COPY ["Shared.Persistence/Shared.Persistence.csproj", "Shared.Persistence/"]
COPY ["AuthModule.Application/AuthModule.Application.csproj", "AuthModule.Application/"]
COPY ["AuthModule.Infrastructure/AuthModule.Infrastructure.csproj", "AuthModule.Infrastructure/"]
COPY ["AuthModule.Domain/AuthModule.Domain.csproj", "AuthModule.Domain/"]
COPY ["UserModule.Domain/UserModule.Domain.csproj", "UserModule.Domain/"]
COPY ["AuthModule.Contracts/AuthModule.Contracts.csproj", "AuthModule.Contracts/"]
COPY ["Shared.Extensions/Shared.Extensions.csproj", "Shared.Extensions/"]
COPY ["UserModule.Contracts/UserModule.Contracts.csproj", "UserModule.Contracts/"]
COPY ["AuthModule.Controlllers/AuthModule.Controlllers.csproj", "AuthModule.Controlllers/"]
COPY ["CompanyModule.Controllers/CompanyModule.Controllers.csproj", "CompanyModule.Controllers/"]
COPY ["CompanyModule.Application/CompanyModule.Application.csproj", "CompanyModule.Application/"]
COPY ["CompanyModule.Contracts/CompanyModule.Contracts.csproj", "CompanyModule.Contracts/"]
COPY ["CompanyModule.Domain/CompanyModule.Domain.csproj", "CompanyModule.Domain/"]
COPY ["DocumentModule.Contracts/DocumentModule.Contracts.csproj", "DocumentModule.Contracts/"]
COPY ["DocumentModule.Domain/DocumentModule.Domain.csproj", "DocumentModule.Domain/"]
COPY ["CompanyModule.Infrastructure/CompanyModule.Infrastructure.csproj", "CompanyModule.Infrastructure/"]
COPY ["CompanyModule.Persistence/CompanyModule.Persistence.csproj", "CompanyModule.Persistence/"]
COPY ["DeanModule.Controllers/DeanModule.Controllers.csproj", "DeanModule.Controllers/"]
COPY ["DeanModule.Application/DeanModule.Application.csproj", "DeanModule.Application/"]
COPY ["DeanModule.Contracts/DeanModule.Contracts.csproj", "DeanModule.Contracts/"]
COPY ["DeanModule.Domain/DeanModule.Domain.csproj", "DeanModule.Domain/"]
COPY ["SelectionModule.Contracts/SelectionModule.Contracts.csproj", "SelectionModule.Contracts/"]
COPY ["SelectionModule.Domain/SelectionModule.Domain.csproj", "SelectionModule.Domain/"]
COPY ["StudentModule.Contracts/StudentModule.Contracts.csproj", "StudentModule.Contracts/"]
COPY ["StudentModule.Domain/StudentModule.Domain.csproj", "StudentModule.Domain/"]
COPY ["DeanModule.Infrastructure/DeanModule.Infrastructure.csproj", "DeanModule.Infrastructure/"]
COPY ["DeanModule.Persistence/DeanModule.Persistence.csproj", "DeanModule.Persistence/"]
COPY ["UserModule.Persistence/UserModule.Persistence.csproj", "UserModule.Persistence/"]
COPY ["UserModule.Infrastructure/UserModule.Infrastructure.csproj", "UserModule.Infrastructure/"]
COPY ["DocumentModule.Controllers/DocumentModule.Controllers.csproj", "DocumentModule.Controllers/"]
COPY ["DocumentModule.Application/DocumentModule.Application.csproj", "DocumentModule.Application/"]
COPY ["DocumentModule.Persistence/DocumentModule.Persistence.csproj", "DocumentModule.Persistence/"]
COPY ["DocumentModule.Infrastructure/DocumentModule.Infrastructure.csproj", "DocumentModule.Infrastructure/"]
COPY ["SelectionModule.Controllers/SelectionModule.Controllers.csproj", "SelectionModule.Controllers/"]
COPY ["SelectionModule.Application/SelectionModule.Application.csproj", "SelectionModule.Application/"]
COPY ["SelectionModule.Infrastructure/SelectionModule.Infrastructure.csproj", "SelectionModule.Infrastructure/"]
COPY ["SelectionModule.Persistence/SelectionModule.Persistence.csproj", "SelectionModule.Persistence/"]
COPY ["PracticeModule.Controllers/PracticeModule.Controllers.csproj", "PracticeModule.Controllers/"]
COPY ["PracticeModule.Application/PracticeModule.Application.csproj", "PracticeModule.Application/"]
COPY ["PracticeModule.Contracts/PracticeModule.Contracts.csproj", "PracticeModule.Contracts/"]
COPY ["PracticeModule.Domain/PracticeModule.Domain.csproj", "PracticeModule.Domain/"]
COPY ["PracticeModule.Infrastructure/PracticeModule.Infrastructure.csproj", "PracticeModule.Infrastructure/"]
COPY ["StudentModule.Controllers/StudentModule.Controllers.csproj", "StudentModule.Controllers/"]
COPY ["StudentModule.Application/StudentModule.Application.csproj", "StudentModule.Application/"]
COPY ["StudentModule.Infrastructure/StudentModule.Infrastructure.csproj", "StudentModule.Infrastructure/"]
COPY ["StudentModule.Persistence/StudentModule.Persistence.csproj", "StudentModule.Persistence/"]
COPY ["UserModule.Controllers/UserModule.Controllers.csproj", "UserModule.Controllers/"]
COPY ["UserModule.Application/UserModule.Application.csproj", "UserModule.Application/"]
COPY ["NotificationModule.Controllers/NotificationModule.Controllers.csproj", "NotificationModule.Controllers/"]
COPY ["NotificationModule.Infrastructure/NotificationModule.Infrastructure.csproj", "NotificationModule.Infrastructure/"]
COPY ["NotificationModule.Domain/NotificationModule.Domain.csproj", "NotificationModule.Domain/"]
COPY ["NotificationModule.Kafka/NotificationModule.Kafka.csproj", "NotificationModule.Kafka/"]
COPY ["NotificationModule.Contracts/NotificationModule.Contracts.csproj", "NotificationModule.Contracts/"]
COPY ["NotificationModule.Persistence/NotificationModule.Persistence.csproj", "NotificationModule.Persistence/"]
RUN dotnet restore "HitsInternship.Api/HitsInternship.Api.csproj"
COPY . .
WORKDIR "/src/dotnet/HitsInternship.Api"
RUN dotnet build "HitsInternship.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HitsInternship.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HitsInternship.Api.dll"]