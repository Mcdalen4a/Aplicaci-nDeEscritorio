-- Creación de la base de datos 
CREATE DATABASE TuBaseDeDatos;
GO

USE TuBaseDeDatos;
GO

-- Tabla Productos
CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Precio DECIMAL(10,2),
    Stock INT
);

INSERT INTO Productos (Nombre, Precio, Stock)
VALUES ('Laptop', 1200.50, 15),
       ('Impresora', 300.00, 8),
       ('Teclado', 50.25, 20);

SELECT * FROM Productos;

-- Tabla Clientes
CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Email NVARCHAR(100),
    Telefono NVARCHAR(20)
);

INSERT INTO Clientes (Nombre, Email, Telefono)
VALUES ('Juan Pérez', 'juan@example.com', '123-456-7890'),
       ('María López', 'maria@example.com', '987-654-3210');

SELECT * FROM Clientes;

-- Tabla Pedidos
CREATE TABLE Pedidos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE,
    ClienteId NVARCHAR(100),
    Total DECIMAL(10,2),
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id) ON DELETE CASCADE
);

INSERT INTO Pedidos (Fecha, ClienteId, Total)
VALUES ('2024-07-17', 1, 500.75),
       ('2024-07-17', 2, 800.20);
	   
SELECT * FROM Pedidos;





DELETE FROM clientes WHERE Id = ;

-- Tabla Reportes
CREATE TABLE Reportes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL,
    Tipo NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(MAX)
);

INSERT INTO Reportes (Fecha, Tipo, Descripcion)
VALUES ('2024-07-17', 'Ventas Diarias', 'Informe de ventas del día.'),
       ('2024-07-16', 'Stock de Productos', 'Resumen del stock actual de productos.');

SELECT * FROM Reportes;

-- Verificar los datos en todas las tablas

-- Permitir valores nulos en la columna 'Nombre' de la tabla 'Clientes'
ALTER TABLE Clientes
ALTER COLUMN Nombre NVARCHAR(100) NULL;

-- Permitir valores nulos en la columna 'Telefono' de la tabla 'Clientes'
ALTER TABLE Clientes
ALTER COLUMN Telefono NVARCHAR(20) NULL;

-- Permitir valores nulos
ALTER TABLE Reportes
ALTER COLUMN Tipo NVARCHAR(50) NULL;

ALTER TABLE Pedidos
ALTER COLUMN Fecha DATE NULL;

ALTER TABLE Productos
ALTER COLUMN Nombre NVARCHAR(100) NULL;

ALTER TABLE Productos 
ALTER COLUMN Precio DECIMAL(10,2) NULL;

ALTER TABLE Productos 
ALTER COLUMN Stock INT NULL;

ALTER TABLE Pedidos
ALTER COLUMN Total DECIMAL(10,2) NULL;

SELECT * FROM Productos;
SELECT * FROM Clientes;
SELECT * FROM Pedidos;
SELECT * FROM Reportes;

CREATE PROC DashboardDatos
    @TotalVentas FLOAT OUT,
    @TotalClientes INT OUT,
    @TotalProductos INT OUT
AS
BEGIN
    -- Obtener el total de ventas
    SELECT @TotalVentas = ISNULL(SUM(Total), 0) 
    FROM Pedidos;

    -- Obtener el total de clientes
    SELECT @TotalClientes = ISNULL(COUNT(Id), 0) 
    FROM Clientes;

    -- Obtener el total de productos
    SELECT @TotalProductos = ISNULL(COUNT(Id), 0) 
    FROM Productos;
END

CREATE PROCEDURE ObtenerVentasPorProducto
AS
BEGIN
SELECT
    p.Nombre,
    SUM(v.Stock) AS TotalVendido
FROM
    Productos p
JOIN
    Productos v ON p.Id = v.Id
GROUP BY
    p.Id, p.Nombre
ORDER BY
    TotalVendido DESC
	END
GO






