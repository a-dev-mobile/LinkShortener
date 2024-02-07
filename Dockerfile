# Используйте базовый образ SDK для сборки и публикации приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Копируйте csproj и восстанавливайте любые зависимости (через NuGet)
COPY *.csproj ./
RUN dotnet restore

# Копируйте проект и соберите его
COPY . ./
RUN dotnet publish -c Release -o out

# Генерируйте образ времени выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 80

# Укажите команду для запуска приложения
ENTRYPOINT ["dotnet", "LinkShortener.dll"]
