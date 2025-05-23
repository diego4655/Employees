# Sistema de Gestión de Empleados

## Descripción
Sistema completo para la gestión de empleados, compuesto por una API REST en .NET y un frontend en Angular.

## Estructura del Proyecto
```
├── Employees/                    # Backend (.NET)
│   └── README.md                # Documentación del backend
│
└── EmployeesFront/              # Frontend (Angular)
    └── employeesfront.client/    # Aplicación Angular
        └── README.md            # Documentación del frontend
```

## Requisitos
- .NET 8.0 SDK
- Node.js
- SQL Server Express
- Angular CLI

## Inicio Rápido

### Backend (.NET)
```bash
cd Employees
dotnet restore
dotnet run
```
API disponible en: `https://localhost:7235`

### Frontend (Angular)
```bash
cd EmployeesFront/employeesfront.client
npm install
ng serve
```
Aplicación disponible en: `http://localhost:53321`

## Documentación
- [Documentación del Backend](./EmployeesFront/EmployeesFront.Server/README.md)
- [Documentación del Frontend](./EmployeesFront/employeesfront.client/README.md)

## Licencia
Este proyecto está bajo la Licencia MIT. 