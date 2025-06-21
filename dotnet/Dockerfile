# Базовый образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Просто копируем всё содержимое текущей директории (dotnet/) внутрь контейнера
COPY . .

# Восстановление зависимостей
RUN dotnet restore "HitsInternship.Api/HitsInternship.Api.csproj"

# Сборка
RUN dotnet build "HitsInternship.Api/HitsInternship.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "HitsInternship.Api/HitsInternship.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HitsInternship.Api.dll"]