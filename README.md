# Pastebin на ASP.NET Core
## Описание

Pastebin — это веб-приложение, позволяющее пользователям загружать и делиться текстовыми фрагментами. Пользователь может указать срок хранения своего фрагмента и по истечении указанного времени фрагмент и метаданные удалятся. Текстовые фрагменты хранятся в объектном хранилище S3, а метаданные в БД. 

## Функциональные возможности

- Создание новых текстовых фрагментов.
- Удаление тектсовых фрагментов.
- Редактирование текстового фрагмента без изменения ссылки.
- Просмотр сохраненных фрагментов.
- Возможность установки времени жизни фрагмента.

## Установка и настройка
### Установка

1. Клонируйте репозиторий:

    ```bash
    git clone https://github.com/0pqbd0/MyPastebin.git
    cd MyPastebin
    ```

2. Настройте соединение с базой данных в файле `appsettings.Development.json`:

    ```json
    "ConnectionStrings": {
    "PastebinDbContext": "User ID=YourUserId;Password=YourPassword;Host=localhost;Port=YourPort;Database=YourDbName;",
    "HangfireConnection": "User ID=YourUserId;Password=YourPassword;Host=localhost;Port=YourPort;Database=YourDbName;"
    },
    ```
    
3. Настройте ключи для объкектного хранилища S3 `appsettings.Development.json`:
 ```json
 "AWS": {
  "BucketName": "YourBucketName",
  "AccessKey": "YourAccessKey",
  "SecretKey": "YourSecretKey"
 }
 ```

4. Примените миграции для создания базы данных:

    ```bash
    dotnet ef database update
    ```

5. Запустите приложение:

    ```bash
    dotnet run
    ```

После запуска приложения, оно будет доступно по адресу `http://localhost:YourPort`.

## Лицензия

Этот проект лицензируется по лицензии MIT. Подробности см. в файле `LICENSE`.
