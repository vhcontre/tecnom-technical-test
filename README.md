# Challenge Técnico: Back-End Dev (NET)

### DeepWiki

[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/vhcontre/tecnom-technical-test)

## NET 8 - WebApi

El proyecto se desarrolló utilizando **NET 8**, una versión LTS (Long-Term Support) ampliamente soportada por el ecosistema .NET.

* URL Swagger: **[http://localhost:7213/swagger/index.html](http://localhost:7213/swagger/index.html)**

## Descripción del Proyecto

* Expone un endpoint **POST /api/leads** que permite crear turnos a talleres.

  * Valida cuestiones básicas (campos requeridos, formatos, etc.)
* Antes de crear el turno, valida que el **place\_id** (id del taller) esté dentro de los talleres activos, consultando una **API externa**.
* Implementa **cache** para evitar consultar la API externa en cada POST de leads.

## Manejo de variables sensibles

Las credenciales y la URL de la API externa están en el archivo de configuración (`appsettings.json` y/o `appsettings.Development.json`). Esto evita exponer datos sensibles en el código y facilita su configuración por entorno. Para producción, se recomienda configurar estos valores con **variables de entorno**.

## Mapeos con AutoMapper

Se utilizan perfiles de **AutoMapper** para mapear entre entidades de dominio y DTOs, incluyendo propiedades simples y objetos anidados.

## Errores detallados en la API externa

El servicio que consume la API externa gestiona errores proporcionando información detallada:

* Código de estado HTTP
* Motivo
* Contenido de la respuesta
* Excepción capturada

Esto facilita el diagnóstico en desarrollo.

## Uso correcto de async/await

Todos los métodos asíncronos usan adecuadamente `async` y `await`. Esto asegura un comportamiento no bloqueante y eficiente en la ejecución de la API.


## Resiliencia con Polly

El proyecto incluye una integración opcional con **Polly** para reintentos automáticos en llamadas HTTP. En entornos compatibles con .NET 8, se puede activar descomentando en `Program.cs`:

```csharp
builder.Services.AddHttpClient("EPlacesApiClient")
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt))
    );
```
---

## Tecnologías, Patrones y Librerías Utilizadas

| Categoría              | Elementos Implementados                                                                                |
| ---------------------- | ------------------------------------------------------------------------------------------------------ |
| **Framework**          | ASP.NET Core (.NET 8), WebAPI                                                                          |
| **Persistencia**       | Entity Framework Core (InMemory)                                                                       |
| **Mapeo de Objetos**   | AutoMapper                                                                                             |
| **Validaciones**       | FluentValidation                                                                                       |
| **Resiliencia**        | Polly (manejo de reintentos en servicios externos)                                                     |
| **Documentación API**  | Swashbuckle (Swagger)                                                                                  |
| **Patrones de Diseño** | Inyección de Dependencias, DTOs, Repository Pattern, Service Layer, Configuración Segura (appsettings) |

---

## Arquitectura y Componentes Principales

* **Controladores:** Manejan los endpoints REST.
* **Servicios:** Contienen la lógica de negocio y reglas de validación.
* **Repositorios:** Manejo del acceso a datos (en memoria).
* **Modelos y DTOs:** Estructuran los datos de dominio y transporte.
* **AutoMapper:** Define mapeos entre entidades y DTOs.
* **Validadores:** FluentValidation asegura la calidad y consistencia de los datos entrantes.
* **API externa:** Validación de talleres activos con cache para mejorar la eficiencia.

## Funcionalidad principal

Permite la **gestión de turnos para talleres**, validando datos ingresados y asegurando que el taller esté activo mediante:

* Validaciones de entrada con FluentValidation
* Mapeo y conversión entre modelos con AutoMapper
* Validación contra API externa de talleres
* Cache para reducir consumo de recursos
* Opcionalmente, resiliencia con Polly
