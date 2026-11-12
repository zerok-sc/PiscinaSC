/*
CREATE DATABASE db_trabajo;
GO
USE db_trabajo;
GO

CREATE TABLE [Piscinas] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Codigo] NVARCHAR(20) NOT NULL,
    [Nombre] NVARCHAR(50) NOT NULL,
    [Capacidad] INT NOT NULL,
    [TarifaBase] DECIMAL(10, 2) NOT NULL,
    [Estado] BIT NOT NULL,
    [FechaCreacion] SMALLDATETIME NOT NULL,
);

CREATE TABLE [Reservas] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Codigo] NVARCHAR(20) NOT NULL,
    [Nombre] NVARCHAR(50) NOT NULL,
    [FechaNacimiento] SMALLDATETIME NOT NULL,
    [Personas] INT NOT NULL,
    [Fecha] SMALLDATETIME NOT NULL,
    [Estado] BIT NOT NULL,
    [Monto] DECIMAL(10, 2) NOT NULL,
    [Piscina] INT NOT NULL,
    CONSTRAINT [FK_Reservas_Piscinas] FOREIGN KEY ([Piscina]) REFERENCES [Piscinas]([Id]) ON DELETE CASCADE,
);

CREATE TABLE [Auditorias] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Fecha] SMALLDATETIME NOT NULL,
    [Tabla] NVARCHAR(50) NOT NULL,
    [Operacion] NVARCHAR(20) NOT NULL,
    [Descripcion] NVARCHAR(500) NOT NULL,
    [Monto] DECIMAL(10, 2) NOT NULL,
    [Exitosa] BIT NOT NULL,
    [Reserva] INT NULL,
    [Piscina] INT NULL,
);
*/
