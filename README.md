### Tecnologías aplicadas al Proyecto

## NET 9 - WebApi
El proyecto se desarrolló utilizando **NET 9**, una versión de **LTS (Long-Term Support)** que ofrece:
- **http://localhost:5156/swagger/index.html**

### Descripción del Proyecto
- Exponga un endpoint POST /api/leads que permita crear turnos a talleres
  - Que valide cuestiones básicas (campos requeridos por ejemplo)

- Previo a creación, debe validar que el place_id (id del taller al cual voy a crearle un turno) esté dentro de los talleres activos.3. Para conocer esos talleres deberá consumir una API externa (detallada en la siguiente
sección)

- Opcional implementar cache para evitar ir a buscar los talleres ante cada nuevo post de leads

