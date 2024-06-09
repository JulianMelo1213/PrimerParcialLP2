# Proyecto de API REST en ASP.NET Core 8

Este proyecto es una API REST desarrollada en ASP.NET Core 8 utilizando el enfoque Database First de Entity Framework Core. La API gestiona un sistema de gestión de inventarios, incluyendo productos, proveedores, almacenes, categorías, entradas, salidas, ajustes e inventarios.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)

## Configuración del Proyecto

### Paso 1: Clonar el Repositorio

```bash
git clone https://github.com/JulianMelo1213/PrimerParcialLP2.git
cd tu-repositorio

### Paso 2: Configurar la Cadena de Conexión
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=GestionInventarios;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}

###Paso 3: Crear la Base de Datos
Ejecuta el script SQL proporcionado para crear la base de datos y las tablas necesarias. El script se encuentra en el archivo GestionInventario.sql.

# Estructura del Proyecto
- Controllers: Contiene los controladores de la API.
- DTOs (Data Transfer Objects): Contiene los objetos de transferencia de datos utilizados para las operaciones CRUD.
- Models: Contiene los modelos de datos que representan las tablas de la base de datos.
- Mapping: Contiene los perfiles de AutoMapper para mapear entre modelos y DTOs.
- Tests: Contiene las pruebas unitarias e integraciones.