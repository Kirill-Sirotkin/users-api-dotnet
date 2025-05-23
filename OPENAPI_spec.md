# users-api-dotnet | v1

> Version 1.0.0

## Path Table

| Method | Path | Description |
| --- | --- | --- |
| POST | [/api/v1/users](#postapiv1users) | Create User with Login, Password, Name, Gender, Birthday and whether the user is admin. [ADMIN ONLY]<br><br>Создание пользователя по логину, паролю, имени, полу и дате рождения + указание будет ли пользователь админом [ТОЛЬКО АДМИН] |
| PATCH | [/api/v1/users/data](#patchapiv1usersdata) | Update User Name, Gender or Birthday. [ADMIN OR CORRESPONDING ACTIVE USER]<br><br>Изменение имени, пола или даты рождения пользователя [АДМИН ИЛИ СООТВЕТСТВУЮЩИЙ ПОЛЬЗОВАТЕЛЬ] |
| PATCH | [/api/v1/users/password](#patchapiv1userspassword) | Update User Password [ADMIN OR CORRESPONDING ACTIVE USER]<br><br>Изменение пароля [АДМИН ИЛИ СООТВЕТСТВУЮЩИЙ ПОЛЬЗОВАТЕЛЬ] |
| PATCH | [/api/v1/users/login](#patchapiv1userslogin) | Update User Login [ADMIN OR CORRESPONDING ACTIVE USER]<br><br>Изменение логина [АДМИН ИЛИ СООТВЕТСТВУЮЩИЙ ПОЛЬЗОВАТЕЛЬ] |
| DELETE | [/api/v1/users/login](#deleteapiv1userslogin) | Deactivate (revoke) User or Delete User [ADMIN ONLY]<br><br>Удаление пользователя по логину полное или мягкое (revoke) [ТОЛЬКО АДМИН] |
| GET | [/api/v1/users/active](#getapiv1usersactive) | Get all Active Users, sorted by CreatedOn [ADMIN ONLY]<br><br>Запрос списка всех активных пользователей, список отсортирован по CreatedOn [ТОЛЬКО АДМИН] |
| GET | [/api/v1/users/by-login](#getapiv1usersby-login) | Get User by Login [ADMIN ONLY]<br><br>Запрос пользователя по логину [ТОЛЬКО АДМИН] |
| POST | [/api/v1/users/by-login-password](#postapiv1usersby-login-password) | Get User by Login and Password. This endpoint also returns a **JWT** required for other endpoints **[NO AUTHENTICATION REQUIRED]**<br><br>Запрос пользователя по логину и паролю. Эта точка доступа также возвращает **JWT**, требуемый для остальных точек доступа **[АУТЕНТИФИКАЦИЯ НЕ ТРЕБУЕТСЯ]** |
| GET | [/api/v1/users/age](#getapiv1usersage) | Get all Users older than specified age [ADMIN ONLY]<br><br>Запрос всех пользователей старше определённого возраста [ТОЛЬКО АДМИН] |
| PATCH | [/api/v1/users/restore](#patchapiv1usersrestore) | Activate deactivated User [ADMIN ONLY]<br><br>Восстановление деактивированного пользователя [ТОЛЬКО АДМИН] |
| POST | [/api/v1/users/helper](#postapiv1usershelper) | Additional helper endpoint that creates a new Admin user. Returns Admin credentials: Login and Password that can be used to get a **JWT** via [/api/v1/users/by-login-password](#postapiv1usersby-login-password) endpoint **[NO AUTHENTICATION REQUIRED]**<br><br>Дополнительная вспомогательная точка доступа, которая создает нового Админа. Возвращает Логин и Пароль Админа, которые могут быть использованы для получения **JWT** при помощи точки доступа [/api/v1/users/by-login-password](#postapiv1usersby-login-password) **[АУТЕНТИФИКАЦИЯ НЕ ТРЕБУЕТСЯ]** |

## Reference Table

| Name | Path |
| --- | --- | 
| CreateUserDto | [#/components/schemas/CreateUserDto](#componentsschemascreateuserdto) |
| LoginDto | [#/components/schemas/LoginDto](#componentsschemaslogindto) |
| LoginPasswordDto | [#/components/schemas/LoginPasswordDto](#componentsschemasloginpassworddto) |
| RevokeUserDto | [#/components/schemas/RevokeUserDto](#componentsschemasrevokeuserdto) |
| UpdateUserDataDto | [#/components/schemas/UpdateUserDataDto](#componentsschemasupdateuserdatadto) |
| UpdateUserLoginDto | [#/components/schemas/UpdateUserLoginDto](#componentsschemasupdateuserlogindto) |
| UpdateUserPasswordDto | [#/components/schemas/UpdateUserPasswordDto](#componentsschemasupdateuserpassworddto) |
| UserDto | [#/components/schemas/UserDto](#componentsschemasuserdto) |
| UserWithJwtDto | [#/components/schemas/UserWithJwtDto](#componentsschemasuserwithjwtdto) |

## Path Details

***

### [POST]/api/v1/users

#### RequestBody

- application/json

```ts
{
  login?: string
  password?: string
  name?: string
  gender?: integer
  birthday?: string
  isAdmin?: boolean
}
```

- text/json

```ts
{
  login?: string
  password?: string
  name?: string
  gender?: integer
  birthday?: string
  isAdmin?: boolean
}
```

- application/*+json

```ts
{
  login?: string
  password?: string
  name?: string
  gender?: integer
  birthday?: string
  isAdmin?: boolean
}
```

#### Responses

- 200 OK

`text/plain`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`application/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`text/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

***

### [PATCH]/api/v1/users/data

#### RequestBody

- application/json

```ts
{
  guid?: string
  name?: string
  gender?: integer
  birthday?: string
}
```

- text/json

```ts
{
  guid?: string
  name?: string
  gender?: integer
  birthday?: string
}
```

- application/*+json

```ts
{
  guid?: string
  name?: string
  gender?: integer
  birthday?: string
}
```

#### Responses

- 200 OK

`text/plain`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`application/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`text/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

***

### [PATCH]/api/v1/users/password

#### RequestBody

- application/json

```ts
{
  guid?: string
  password?: string
}
```

- text/json

```ts
{
  guid?: string
  password?: string
}
```

- application/*+json

```ts
{
  guid?: string
  password?: string
}
```

#### Responses

- 200 OK

`text/plain`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`application/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`text/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

***

### [PATCH]/api/v1/users/login

#### RequestBody

- application/json

```ts
{
  guid?: string
  login?: string
}
```

- text/json

```ts
{
  guid?: string
  login?: string
}
```

- application/*+json

```ts
{
  guid?: string
  login?: string
}
```

#### Responses

- 200 OK

`text/plain`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`application/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`text/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

***

### [DELETE]/api/v1/users/login

#### RequestBody

- application/json

```ts
{
  login?: string
  isDeleteFully?: boolean
}
```

- text/json

```ts
{
  login?: string
  isDeleteFully?: boolean
}
```

- application/*+json

```ts
{
  login?: string
  isDeleteFully?: boolean
}
```

#### Responses

- 200 OK

`text/plain`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`application/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`text/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

***

### [GET]/api/v1/users/active

#### Responses

- 200 OK

`text/plain`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}[]
```

`application/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}[]
```

`text/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}[]
```

***

### [GET]/api/v1/users/by-login

#### Parameters(Query)

```ts
login?: string
```

#### Responses

- 200 OK

`text/plain`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`application/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`text/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

***

### [POST]/api/v1/users/by-login-password

#### RequestBody

- application/json

```ts
{
  login?: string
  password?: string
}
```

- text/json

```ts
{
  login?: string
  password?: string
}
```

- application/*+json

```ts
{
  login?: string
  password?: string
}
```

#### Responses

- 200 OK

`text/plain`

```ts
{
  jwt: string
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`application/json`

```ts
{
  jwt: string
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`text/json`

```ts
{
  jwt: string
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

***

### [GET]/api/v1/users/age

#### Parameters(Query)

```ts
age?: string
```

#### Responses

- 200 OK

`text/plain`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}[]
```

`application/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}[]
```

`text/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}[]
```

***

### [PATCH]/api/v1/users/restore

#### RequestBody

- application/json

```ts
{
  login?: string
}
```

- text/json

```ts
{
  login?: string
}
```

- application/*+json

```ts
{
  login?: string
}
```

#### Responses

- 200 OK

`text/plain`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`application/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

`text/json`

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

***

### [POST]/api/v1/users/helper

#### Responses

- 200 OK

`text/plain`

```ts
{
  login?: string
  password?: string
}
```

`application/json`

```ts
{
  login?: string
  password?: string
}
```

`text/json`

```ts
{
  login?: string
  password?: string
}
```

## References

### #/components/schemas/CreateUserDto

```ts
{
  login?: string
  password?: string
  name?: string
  gender?: integer
  birthday?: string
  isAdmin?: boolean
}
```

### #/components/schemas/LoginDto

```ts
{
  login?: string
}
```

### #/components/schemas/LoginPasswordDto

```ts
{
  login?: string
  password?: string
}
```

### #/components/schemas/RevokeUserDto

```ts
{
  login?: string
  isDeleteFully?: boolean
}
```

### #/components/schemas/UpdateUserDataDto

```ts
{
  guid?: string
  name?: string
  gender?: integer
  birthday?: string
}
```

### #/components/schemas/UpdateUserLoginDto

```ts
{
  guid?: string
  login?: string
}
```

### #/components/schemas/UpdateUserPasswordDto

```ts
{
  guid?: string
  password?: string
}
```

### #/components/schemas/UserDto

```ts
{
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```

### #/components/schemas/UserWithJwtDto

```ts
{
  jwt: string
  guid?: string
  login?: string
  name?: string
  gender?: integer
  birthday?: string
  createdOn?: string
  modifiedOn?: string
  isActive?: boolean
}
```
