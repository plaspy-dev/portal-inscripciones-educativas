USE portal_demo;
CREATE TABLE dbo.usuarios2 (id int PRIMARY KEY, login varchar(40), clave varchar(80), grupo varchar(16),
  vigencias varchar(30), nombre nvarchar(80), usesion datetime NULL, estado int);
INSERT INTO dbo.usuarios2 VALUES (501,'alicia_demo','$(FictionalPortalPassword)','2','2008',N'Alicia Ejemplo',NULL,1);
GRANT SELECT, UPDATE ON dbo.usuarios2 TO portal_demo_user;
CREATE TABLE dbo.usuariosentes2 (usuario int, enteterritorial int, vigencia int);
CREATE TABLE dbo.eterritoriales (codigo int PRIMARY KEY, nombre nvarchar(80));
CREATE TABLE dbo.proyectosi (eterritorial int, vigencia int);
CREATE TABLE dbo.proyectos (eterritorial int, vigencia int);
CREATE TABLE dbo.conveniosm (eterritorial int, vigencia int);
CREATE TABLE dbo.Menus (id int PRIMARY KEY, nombre nvarchar(80), descripcion nvarchar(120),
  url varchar(200), padre int, orden int);
CREATE TABLE dbo.gruposupermisos (grupo varchar(16), menu int, permisos int);
INSERT INTO dbo.eterritoriales VALUES (901,N'Entidad Ejemplo');
INSERT INTO dbo.usuariosentes2 VALUES (501,901,2008);
INSERT INTO dbo.Menus VALUES (902,N'Catálogo de prueba',N'Consulta del catálogo ficticio','/datosmapa.aspx',0,1);
INSERT INTO dbo.gruposupermisos VALUES ('2',902,1);
GRANT SELECT ON dbo.usuariosentes2 TO portal_demo_user;
GRANT SELECT ON dbo.eterritoriales TO portal_demo_user;
GRANT SELECT ON dbo.proyectosi TO portal_demo_user;
GRANT SELECT ON dbo.proyectos TO portal_demo_user;
GRANT SELECT ON dbo.conveniosm TO portal_demo_user;
GRANT SELECT ON dbo.Menus TO portal_demo_user;
GRANT SELECT ON dbo.gruposupermisos TO portal_demo_user;
GO
