# Users API

[ReadMe на русском](README_ru.md)

## Description

This project aims to provide a backend API to manage a User entity

## Features

- Swagger UI available for easy interaction with the API
- 10 available endpoints for a variety of ways to manage the User entity
- 1 helper endpoint for convenient first Admin user creation

>**Note:**
>Full OpenApi specification can be found [here](./OPENAPI_spec.md).

## Technical features

- Data storage using PostgreSQL
- Authentication and authorization system with JSON Web Token

>**Note:**
>Most endpoints require JWT Bearer Authorization token to work. The JWT is generated and returned with the [/api/v1/users/by-login-password](./OPENAPI_spec.md#postapiv1usersby-login-password) endpoint.

## Setup

### Key prerequisites

- PostgreSQL service setup and running on your system
- ```.env``` file in the project root folder with the following environment variables:
    - ```DATABASE_URL``` - URL to connect to the PostgreSQL database. The example is provided below; replace ```YOUR_...``` variables with your own settings.
    ```
    DATABASE_URL="Host=YOUR_HOST;Port=YOUR_PORT;Username=YOUR_USERNAME;Password=YOUR_PASSWORD;Database=usersApi"
    ```
    - ```JWT_SECRET``` - key to sign the JWTs. Make sure the secret string is fairly long, as the cryptography algorithm demands that (for example, 120 characters).
    ```
    JWT_SECRET="YOUR_LONG_SECRET_STRING"
    ```
    - ```JWT_EXPIRES_IN_MINUTES``` - lifetime of a JWT in minutes. Can be any number of minutes. 
    ```
    JWT_EXPIRES_IN_MINUTES="5"
    ```

### Necessary commands (in order)

1. ```dotnet restore``` - install necessary dependencies.
2. ```dotnet ef database update``` - apply the database migrations to your PostgreSQL. This will create a ```usersApi``` database and a ```Users``` table in Postgres. This step can be skipped if you create the database manually with the necessary User entity table.
3. ```dotnet watch run``` - run the API in watch mode. After this, the Swagger UI will open in your browser (if not, by default it can be accessed at http://localhost:5148/swagger/index.html) and the API endpoints will become available.