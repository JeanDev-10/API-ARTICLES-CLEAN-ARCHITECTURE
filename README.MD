# Articles Clean Architecture API 📚

Una Web API construida con **ASP.NET Core 8.0** siguiendo los principios de **Clean Architecture** para la gestión de artículos con operaciones CRUD completas y validaciones robustas.

## ✨ Características

- ✅ **CRUD Completo** de Artículos y Categorías (Crear, Leer, Actualizar, Eliminar)
- ✅ **Clean Architecture** con separación clara de responsabilidades
- ✅ **Validaciones robustas** con FluentValidation
- ✅ **Reglas de negocio** encapsuladas en el Domain
- ✅ **Entity Framework Core** con migraciones
- ✅ **Manejo profesional de errores** con códigos HTTP apropiados
- ✅ **Documentación automática** con Swagger/OpenAPI
- ✅ **Inyección de dependencias** configurada correctamente
- ✅ **Repository Pattern** para acceso a datos
- ✅ **DTOs** para transferencia de datos

### Reglas de Negocio Implementadas

- **Nombre único**: No se pueden repetir nombres de artículos
- **Precio positivo**: El precio debe ser mayor a 0
- **Validaciones de longitud**: Nombre (3-100 chars), Descripción (10-500 chars)
- **Validaciones personalizadas**: Caracteres permitidos, detección de contenido inapropiado
- **Validaciones condicionales**: Precios consistentes con el tipo de producto

## 🏗️ Arquitectura

Este proyecto implementa **Clean Architecture** de Uncle Bob con 4 capas principales:

```
┌─────────────────────────────────────────┐
│              WebApi (API Layer)         │
│  Controllers, Program.cs, Middleware   │
└─────────────────────────────────────────┘
                    │
┌─────────────────────────────────────────┐
│     Articles.Infrastructure             │
│   Repositories, DbContext, Data Access │
└─────────────────────────────────────────┘
                    │
┌─────────────────────────────────────────┐
│      Articles.Application               │
│  Services, DTOs, Validators, Interfaces │
└─────────────────────────────────────────┘
                    │
┌─────────────────────────────────────────┐
│        Articles.Domain                  │
│      Entities, Business Rules          │
└─────────────────────────────────────────┘
```

### Principios Aplicados

- **Dependency Inversion**: Las dependencias apuntan hacia adentro
- **Single Responsibility**: Cada capa tiene una responsabilidad específica
- **Open/Closed**: Extensible sin modificar código existente
- **Interface Segregation**: Interfaces específicas y cohesivas

## 🛠️ Tecnologías y Herramientas

### Framework y Runtime

- **.NET 8.0** - Framework principal
- **ASP.NET Core 8.0** - Web API framework

### Base de Datos y ORM

- **Entity Framework Core 8.0.19** - ORM principal
- **Microsoft.EntityFrameworkCore.SqlServer 8.0.19** - Provider para SQL Server
- **Microsoft.EntityFrameworkCore.Design 8.0.19** - Herramientas de diseño
- **Microsoft.EntityFrameworkCore.Tools 8.0.19** - Herramientas CLI

### Validaciones

- **FluentValidation 12.0.0** - Validaciones robustas y expresivas
- **FluentValidation.AspNetCore 11.3.1** - Integración con ASP.NET Core
- **FluentValidation.DependencyInjectionExtensions 12.0.0** - Inyección de dependencias

### Documentación y Testing

- **Swashbuckle.AspNetCore 6.6.2** - Swagger/OpenAPI documentation

### Herramientas de Desarrollo

- **Visual Studio / VS Code** - IDEs recomendados
- **REST Client** - Para pruebas de API (archivo .http)
- **SQL Server Management Studio** - Gestión de base de datos
- **dotnet CLI** - Herramientas de línea de comandos

## 🚀 Instalación y Configuración

### Prerrequisitos

- **.NET 8.0 SDK** instalado
- **SQL Server** (LocalDB es suficiente para desarrollo)
- **Git** para clonar el repositorio

### Pasos de Instalación

1. **Clonar el repositorio**

```bash
git clone https://github.com/JeanDev-10/API-ARTICLES-CLEAN-ARCHITECTURE
cd ArticlesCleanArchitecture
```

2. **Restaurar dependencias**

```bash
dotnet restore
```

3. **Configurar la base de datos**

```bash
# Actualizar connection string en WebApi/appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ArticlesDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

4. **Crear y aplicar migraciones**

```bash


# Aplicar migración a la base de datos
dotnet ef database update --project Articles.Infrastructure --startup-project WebApi
```

5. **Ejecutar la aplicación**

```bash
dotnet run --project WebApi
```

6. **Acceder a la documentación**

- Swagger UI: `https://localhost:5227/swagger`
- API Base URL: `https://localhost:5227/api`

## 🔧 Uso de la API

### Endpoints Disponibles

| Método | Endpoint             | Descripción                   |
| ------ | -------------------- | ----------------------------- |
| GET    | `/api/articles`      | Obtener todos los artículos   |
| GET    | `/api/articles/{id}` | Obtener artículo por ID       |
| POST   | `/api/articles`      | Crear nuevo artículo          |
| PUT    | `/api/articles/{id}` | Actualizar artículo existente |
| DELETE | `/api/articles/{id}` | Eliminar artículo             |

### Ejemplo de Uso

#### Crear un Artículo

```http
POST https://localhost:5001/api/articles
Content-Type: application/json

{
    "name": "Laptop Gaming",
    "price": 1500.99,
    "description": "Laptop para gaming de alta gama con RTX 4070"
}
```

#### Respuesta Exitosa (201 Created)

```json
{
  "id": 1,
  "name": "Laptop Gaming",
  "price": 1500.99,
  "description": "Laptop para gaming de alta gama con RTX 4070",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T10:30:00Z"
}
```

#### Actualizar un Artículo

```http
PUT https://localhost:5001/api/articles/1
Content-Type: application/json

{
    "name": "Laptop Gaming Pro",
    "price": 1699.99,
    "description": "Laptop gaming profesional con RTX 4070 y 32GB RAM"
}
```

## ✅ Validaciones

### Validaciones Implementadas

#### Nombre del Artículo

- ✅ **Requerido**: No puede estar vacío
- ✅ **Longitud**: Entre 3 y 100 caracteres
- ✅ **Único**: No se pueden repetir nombres
- ✅ **Caracteres válidos**: Solo letras, números, espacios y algunos símbolos
- ✅ **Contenido apropiado**: Sin palabras inapropiadas

#### Precio

- ✅ **Positivo**: Debe ser mayor a 0
- ✅ **Máximo**: No puede exceder $999,999.99
- ✅ **Decimales**: Máximo 2 decimales
- ✅ **Consistencia**: Debe ser consistente con el tipo de producto

#### Descripción

- ✅ **Requerida**: No puede estar vacía
- ✅ **Longitud**: Entre 10 y 500 caracteres
- ✅ **Específica**: No debe ser genérica o muy básica

### Códigos de Respuesta HTTP

- **200 OK** - Operación exitosa
- **201 Created** - Recurso creado exitosamente
- **204 No Content** - Eliminación exitosa
- **400 Bad Request** - Errores de validación
- **404 Not Found** - Recurso no encontrado
- **409 Conflict** - Conflicto (ej: nombre duplicado)
- **500 Internal Server Error** - Error del servidor

### Ejemplo de Error de Validación

```json
{
  "message": "Errores de validación",
  "errors": [
    "El nombre del artículo es requerido",
    "El precio debe ser mayor a 0",
    "La descripción debe tener entre 10 y 500 caracteres"
  ]
}
```

## 🧪 Testing

### Archivo de Pruebas HTTP

El proyecto incluye un archivo `WebApi.http` con casos de prueba que cubren:

- ✅ **CRUD básico** - Todas las operaciones principales
- ✅ **Validaciones de negocio** - Todos los casos de error
- ✅ **Casos límite** - Longitudes máximas, caracteres especiales
- ✅ **Manejo de errores** - JSON malformado, headers incorrectos

#### Ejecutar Pruebas

1. **Con Visual Studio Code**:

   - Instalar extensión "REST Client"
   - Abrir `articles-api-tests.http`
   - Hacer clic en "Send Request" sobre cada prueba

2. **Con otras herramientas**:
   - **Postman**: Importar las requests
   - **Insomnia**: Usar las requests HTTP
   - **curl**: Convertir a comandos curl

## 🔧 Uso de la API

### Endpoints Disponibles

| Método | Endpoint             | Descripción                   |
| ------ | -------------------- | ----------------------------- |
| GET    | `/api/articles`      | Obtener todos los artículos   |
| GET    | `/api/articles/{id}` | Obtener artículo por ID       |
| POST   | `/api/articles`      | Crear nuevo artículo          |
| PUT    | `/api/articles/{id}` | Actualizar artículo existente |
| DELETE | `/api/articles/{id}` | Eliminar artículo             |

#### Categorías

| Método | Endpoint               | Descripción                    |
| ------ | ---------------------- | ------------------------------ |
| GET    | `/api/categories`      | Obtener todas las categorías   |
| GET    | `/api/categories/{id}` | Obtener categoría por ID       |
| POST   | `/api/categories`      | Crear nueva categoría          |
| PUT    | `/api/categories/{id}` | Actualizar categoría existente |
| DELETE | `/api/categories/{id}` | Eliminar categoría             |

---

## Autor

- [Jean Pierre Rodríguez Zambrano](https://github.com/JeanDev-10)

---
