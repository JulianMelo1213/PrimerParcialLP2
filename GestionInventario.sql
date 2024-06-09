-- Crear la base de datos
CREATE DATABASE GestionInventarios;
GO

-- Usar la base de datos reci�n creada
USE GestionInventarios;
GO

-- Crear la tabla Producto
CREATE TABLE Producto (
    ProductoId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    Precio DECIMAL(18, 2) NOT NULL
);
GO

-- Crear la tabla Proveedor
CREATE TABLE Proveedor (
    ProveedorId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Direccion NVARCHAR(255),
    Telefono NVARCHAR(15)
);
GO

-- Crear la tabla Almacen
CREATE TABLE Almacen (
    AlmacenId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Ubicacion NVARCHAR(255)
);
GO

-- Crear la tabla Categoria
CREATE TABLE Categoria (
    CategoriaId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255)
);
GO

-- Crear la tabla Entrada
CREATE TABLE Entrada (
    EntradaId INT PRIMARY KEY IDENTITY(1,1),
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    Fecha DATETIME NOT NULL,
    FOREIGN KEY (ProductoId) REFERENCES Producto(ProductoId)
);
GO

-- Crear la tabla Salida
CREATE TABLE Salida (
    SalidaId INT PRIMARY KEY IDENTITY(1,1),
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    Fecha DATETIME NOT NULL,
    FOREIGN KEY (ProductoId) REFERENCES Producto(ProductoId)
);
GO

-- Crear la tabla Ajuste
CREATE TABLE Ajuste (
    AjusteId INT PRIMARY KEY IDENTITY(1,1),
    ProductoId INT NOT NULL,
    AlmacenId INT NOT NULL,
    Cantidad INT NOT NULL,
    Fecha DATETIME NOT NULL,
    Tipo NVARCHAR(50) NOT NULL,
    FOREIGN KEY (ProductoId) REFERENCES Producto(ProductoId),
    FOREIGN KEY (AlmacenId) REFERENCES Almacen(AlmacenId)
);
GO

-- Crear la tabla Inventario
CREATE TABLE Inventario (
    InventarioId INT PRIMARY KEY IDENTITY(1,1),
    ProductoId INT NOT NULL,
    AlmacenId INT NOT NULL,
    Cantidad INT NOT NULL,
    Fecha DATETIME NOT NULL,
    FOREIGN KEY (ProductoId) REFERENCES Producto(ProductoId),
    FOREIGN KEY (AlmacenId) REFERENCES Almacen(AlmacenId)
);
GO

-- Crear tabla intermedia ProductoProveedor para la relaci�n muchos a muchos
CREATE TABLE ProductoProveedor (
    ProductoId INT NOT NULL,
    ProveedorId INT NOT NULL,
    PRIMARY KEY (ProductoId, ProveedorId),
    FOREIGN KEY (ProductoId) REFERENCES Producto(ProductoId),
    FOREIGN KEY (ProveedorId) REFERENCES Proveedor(ProveedorId)
);
GO

-- Crear tabla intermedia ProductoCategoria para la relaci�n muchos a muchos
CREATE TABLE ProductoCategoria (
    ProductoId INT NOT NULL,
    CategoriaId INT NOT NULL,
    PRIMARY KEY (ProductoId, CategoriaId),
    FOREIGN KEY (ProductoId) REFERENCES Producto(ProductoId),
    FOREIGN KEY (CategoriaId) REFERENCES Categoria(CategoriaId)
);
GO

INSERT INTO Producto (Nombre, Descripcion, Precio) VALUES ('Producto A', 'Descripci�n del Producto A', 10.50);
INSERT INTO Producto (Nombre, Descripcion, Precio) VALUES ('Producto B', 'Descripci�n del Producto B', 20.00);
INSERT INTO Producto (Nombre, Descripcion, Precio) VALUES ('Producto C', 'Descripci�n del Producto C', 15.75);

INSERT INTO Proveedor (Nombre, Direccion, Telefono) VALUES ('Proveedor 1', 'Direcci�n del Proveedor 1', '123456789');
INSERT INTO Proveedor (Nombre, Direccion, Telefono) VALUES ('Proveedor 2', 'Direcci�n del Proveedor 2', '987654321');
INSERT INTO Proveedor (Nombre, Direccion, Telefono) VALUES ('Proveedor 3', 'Direcci�n del Proveedor 3', '456123789');

INSERT INTO Almacen (Nombre, Ubicacion) VALUES ('Almac�n Central', 'Ubicaci�n del Almac�n Central');
INSERT INTO Almacen (Nombre, Ubicacion) VALUES ('Almac�n Norte', 'Ubicaci�n del Almac�n Norte');
INSERT INTO Almacen (Nombre, Ubicacion) VALUES ('Almac�n Sur', 'Ubicaci�n del Almac�n Sur');

INSERT INTO Categoria (Nombre, Descripcion) VALUES ('Categor�a 1', 'Descripci�n de la Categor�a 1');
INSERT INTO Categoria (Nombre, Descripcion) VALUES ('Categor�a 2', 'Descripci�n de la Categor�a 2');
INSERT INTO Categoria (Nombre, Descripcion) VALUES ('Categor�a 3', 'Descripci�n de la Categor�a 3');

INSERT INTO Entrada (ProductoId, Cantidad, Fecha) VALUES (1, 100, '2024-06-01 10:00:00');
INSERT INTO Entrada (ProductoId, Cantidad, Fecha) VALUES (2, 150, '2024-06-02 11:00:00');
INSERT INTO Entrada (ProductoId, Cantidad, Fecha) VALUES (3, 200, '2024-06-03 12:00:00');

INSERT INTO Salida (ProductoId, Cantidad, Fecha) VALUES (1, 50, '2024-06-04 13:00:00');
INSERT INTO Salida (ProductoId, Cantidad, Fecha) VALUES (2, 75, '2024-06-05 14:00:00');
INSERT INTO Salida (ProductoId, Cantidad, Fecha) VALUES (3, 100, '2024-06-06 15:00:00');

INSERT INTO Ajuste (ProductoId, AlmacenId, Cantidad, Fecha, Tipo) VALUES (1, 1, 10, '2024-06-07 16:00:00', 'Incremento');
INSERT INTO Ajuste (ProductoId, AlmacenId, Cantidad, Fecha, Tipo) VALUES (2, 2, -5, '2024-06-08 17:00:00', 'Decremento');
INSERT INTO Ajuste (ProductoId, AlmacenId, Cantidad, Fecha, Tipo) VALUES (3, 3, 20, '2024-06-09 18:00:00', 'Incremento');

INSERT INTO Inventario (ProductoId, AlmacenId, Cantidad, Fecha) VALUES (1, 1, 500, '2024-06-10 19:00:00');
INSERT INTO Inventario (ProductoId, AlmacenId, Cantidad, Fecha) VALUES (2, 2, 300, '2024-06-11 20:00:00');
INSERT INTO Inventario (ProductoId, AlmacenId, Cantidad, Fecha) VALUES (3, 3, 400, '2024-06-12 21:00:00');

INSERT INTO ProductoProveedor (ProductoId, ProveedorId) VALUES (1, 1);
INSERT INTO ProductoProveedor (ProductoId, ProveedorId) VALUES (2, 2);
INSERT INTO ProductoProveedor (ProductoId, ProveedorId) VALUES (3, 3);

INSERT INTO ProductoCategoria (ProductoId, CategoriaId) VALUES (1, 1);
INSERT INTO ProductoCategoria (ProductoId, CategoriaId) VALUES (2, 2);
INSERT INTO ProductoCategoria (ProductoId, CategoriaId) VALUES (3, 3);

select * from Ajuste