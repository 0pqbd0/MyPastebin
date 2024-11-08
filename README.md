# Pastebin на ASP.NET Core
## Описание

Pastebin — это веб-приложение, позволяющее пользователям загружать и делиться текстовыми фрагментами. Пользователь может указать срок хранения своего фрагмента и по истечении указанного времени фрагмент и метаданные удалятся. Текстовые фрагменты хранятся в объектном хранилище S3, а метаданные в БД. 

## Функциональные возможности

- Создание новых текстовых фрагментов.
- Удаление текстовых фрагментов.
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

## ToDo

- [ ] **Аутентификация пользователей:**
  - [ ] Добавить возможность регистрации и входа пользователей.
  - [ ] Ограничить доступ к приватным фрагментам только для авторизованных пользователей.

- [ ] **Микросервис для генерации хэша:**
  - [ ] Создание короткого URL.
  - [ ] Заранее генерировать набор из хэшей.

- [ ] **Статистика и аналитика:**
  - [ ] Внедрить систему отслеживания количества просмотров каждого фрагмента.

- [ ] **Расширенные настройки фрагментов:**
  - [ ] Возможность установки пароля на фрагмент.
  - [ ] Поддержка режима самоудаления фрагмента после первого просмотра.

- [ ] **Тестирование и CI/CD:**
  - [ ] Написать юнит-тесты для основных компонентов.
  - [ ] Настроить CI/CD с использованием GitHub Actions.

- [ ] **Оптимизация производительности:**
  - [ ] Кэширование часто запрашиваемых фрагментов и их метаданные (Redis).

- [ ] **Балансировщик нагрузки**

- [ ] **Пользовательский интерфейс**


## Лицензия

Этот проект лицензируется по лицензии MIT. Подробности см. в файле `LICENSE`.
