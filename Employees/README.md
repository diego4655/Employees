# Employees - API de Gestión de Empleados

## Descripción
API REST desarrollada con .NET Core para la gestión de empleados, implementando arquitectura CQRS y Entity Framework Core.

## Características
- API RESTful
- Arquitectura CQRS
- Entity Framework Core
- SQL Server Express
- Swagger para documentación

## Requisitos
- .NET 8.0 SDK
- SQL Server Express
- Visual Studio 2022 (recomendado)

## Instalación

1. Clonar el repositorio:
```bash
git clone [URL_DEL_REPOSITORIO]
cd Employees
```

2. Restaurar dependencias:
```bash
dotnet restore
```

3. Configurar la base de datos:
   - Asegurarse que SQL Server Express esté instalado y corriendo
   - Actualizar la cadena de conexión en `appsettings.json`

4. Ejecutar las migraciones:
```bash
dotnet ef database update
```

5. Iniciar la aplicación:
```bash
dotnet run
```

La API estará disponible en `https://localhost:7235`
La documentación Swagger en `https://localhost:7235/swagger`

## Estructura del Proyecto
```
Employees/
├── Application/          # Capa de aplicación (CQRS)
│   ├── Commands/        # Comandos
│   └── Queries/         # Consultas
├── Domain/              # Entidades y lógica de dominio
├── Infrastructure/      # Implementaciones de infraestructura
└── Controllers/         # Controladores API
```

## Tecnologías
- .NET 8
- Entity Framework Core
- MediatR
- SQL Server
- Swagger/OpenAPI

## Endpoints Principales
- GET /api/employees - Obtener todos los empleados
- GET /api/employees/{id} - Obtener empleado por ID
- POST /api/employees - Crear nuevo empleado
- PUT /api/employees/{id} - Actualizar empleado
- DELETE /api/employees/{id} - Eliminar empleado

## Desarrollo
1. Crear una rama para nueva funcionalidad:
```bash
git checkout -b feature/nueva-caracteristica
```

2. Realizar cambios y commits:
```bash
git add .
git commit -m "Descripción de los cambios"
```

3. Enviar cambios:
```bash
git push origin feature/nueva-caracteristica
```

## Contribución
1. Fork el proyecto
2. Crear una rama
3. Realizar cambios
4. Enviar Pull Request

## Soporte
Para reportar problemas o solicitar nuevas características, crear un issue en el repositorio.

## Licencia
Este proyecto está bajo la Licencia MIT. 