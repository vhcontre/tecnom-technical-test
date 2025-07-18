### Tecnologías aplicadas al Proyecto

## NET 8 - WebApi
El proyecto se desarrolló utilizando **NET 8**, una versión LTS (Long-Term Support) ampliamente soportada por el ecosistema .NET.
- **http://localhost:7213/swagger/index.html**

### Descripción del Proyecto
- Expone un endpoint POST /api/leads que permite crear turnos a talleres
  - Valida cuestiones básicas (campos requeridos por ejemplo)
- Previo a la creación, valida que el place_id (id del taller al cual se va a crear el turno) esté dentro de los talleres activos. Para conocer esos talleres se consume una API externa.
- Implementa cache para evitar ir a buscar los talleres ante cada nuevo post de leads.

### Manejo de variables sensibles

Las credenciales y la URL de la API externa se movieron al archivo de configuración (`appsettings.json` y/o `appsettings.Development.json`). Esto permite mantener los datos sensibles fuera del código fuente y facilita su gestión en diferentes entornos. Para producción, se recomienda utilizar variables de entorno.

### Resiliencia con Polly

El proyecto incluye un ejemplo de cómo se puede utilizar Polly para agregar una política de reintentos automáticos en el HttpClient. En este entorno, la extensión no está disponible por cuestiones de compatibilidad, pero si se prueba en otro proyecto .NET 8 compatible, solo es necesario descomentar la línea en `Program.cs`:

```csharp
// Si el entorno lo soporta, así se agrega una política de reintentos con Polly:
builder.Services.AddHttpClient("EPlacesApiClient")
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt))
    );
```

De esta forma, el HttpClient utilizado por `EPlacesService` realizará reintentos automáticos ante fallos transitorios de la API externa. En este repositorio la línea permanece comentada para asegurar la compilación.

### Mapeos con AutoMapper

El proyecto utiliza perfiles de AutoMapper para mapear todos los campos importantes entre entidades y DTOs en ambos sentidos. Los mapeos cubren propiedades simples y objetos anidados, asegurando que la conversión de datos sea completa y precisa en las operaciones de la API.

### Errores detallados en la API externa

El servicio queconsume la API externa registra y lanza errores con información detallada (código de estado, motivo, contenido de respuesta y excepción) cuando ocurre una falla. Esto facilita el debugging y la identificación rápida de problemas en la integración.

### Uso correcto de async/await

Todos los métodos asíncronos del proyecto utilizan correctamente `async` y `await`, evitando llamadas bloqueantes como `.Result` o `.Wait()`. Esto garantiza un manejo eficiente de las operaciones asíncronas y previene bloqueos innecesarios en la ejecución de la API.

