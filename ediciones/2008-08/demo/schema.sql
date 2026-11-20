CREATE DATABASE portal_demo;
GO
USE portal_demo;
CREATE TABLE dbo.departamentos (
  codigo varchar(10) PRIMARY KEY, nombre nvarchar(100), mapa varchar(30),
  color int, observaciones nvarchar(250)
);
INSERT INTO dbo.departamentos VALUES ('901', N'Departamento Ejemplo', 'demo901', 0, N'Registro ficticio');
GO
