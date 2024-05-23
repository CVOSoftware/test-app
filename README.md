# Реализация тестового задания (AGSR)

## Инструменты
- **Visual Studio 2022** - среда разработки
- **Docker** - сревис контейнеризации
- **Postman** - утилита для тестирования WEB API

## Инфраструктура

- **postgres:15.4** - реляционная база дыннх
- **pgadmin4 (Web)** - веб клиент для работы с базой данныхв графическом режиме

## Права доступа
- postgres:
    - **user** - developer
    - **password** - 1111
    - **container host** - testapp.postgres:5432
- pgadmin:
   - **email** - developer@dev.com
   - **password** - 1111
   - **host** - 127.0.0.1:18080
   - **container host** - testapp.pgadmin:80

## Структура репозитория
- **docs** - файлы с коллекцией запросов для **Postman**
- **src** - исходный код

## Приложения

- TestApp.Host - API сервис
- TestApp.Data.Bootstrap - утилита инициализации базы данных тестовыми данными

## TestApp.Data.Bootstrap

Команды и опции:

- **init** - комада инициализации тестовыми
    - **-h(--host)** - адресс API сервиса
    - **-c(--count)** - количество данных для генерации (1 - 255)