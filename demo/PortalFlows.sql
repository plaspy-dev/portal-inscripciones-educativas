CREATE DATABASE portal_demo;
GO
USE portal_demo;
CREATE TABLE dbo.[menus] (
	id INT NOT NULL IDENTITY(1,1),
	nombre VARCHAR(255),
	descripcion VARCHAR(255),
	url VARCHAR(255),
	padre INT REFERENCES menus(id) DEFAULT NULL,
	PRIMARY KEY(id) 
);
CREATE TABLE dbo.[varios] (
	id BIGINT NOT NULL IDENTITY(1,1),
	nombre VARCHAR(255),
	descripcion TEXT,
	tipo INT NOT NULL,
	nReservado BIGINT DEFAULT 0,
	szReservado TEXT DEFAULT '',
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[cuentasbancarias] (
	id BIGINT NOT NULL IDENTITY(1,1),
	numero VARCHAR(255),
	tipo BIGINT REFERENCES varios(id),
	entidad BIGINT REFERENCES varios(id),
	sucursal VARCHAR(255),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[gruposupermisos] (
	
	id INT NOT NULL IDENTITY(1,1),
	grupo BIGINT REFERENCES varios(id),
	menu  INT REFERENCES menus(id),
	permisos INT NOT NULL,
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[departamentos] (

	codigo BIGINT NOT NULL,
	nombre VARCHAR(255) NOT NULL,
	mapa VARCHAR(255),
	observaciones TEXT,
	color INT,
	PRIMARY KEY (codigo)
);
CREATE TABLE dbo.[eterritoriales] (
	codigo BIGINT NOT NULL,
	nombre VARCHAR(255),
	region BIGINT REFERENCES varios(id),
	nReservado INT DEFAULT 0,
	PRIMARY KEY(codigo)
);
CREATE TABLE dbo.[municipios] (
	id BIGINT NOT NULL IDENTITY(1,1),
	codigo BIGINT NOT NULL,
	nombre VARCHAR(255),
	departamento BIGINT NOT NULL REFERENCES departamentos(codigo),
	eterritorial BIGINT REFERENCES eterritoriales(codigo),
	totalhabitantes BIGINT,
	region BIGINT REFERENCES varios(id),
	grupo BIGINT REFERENCES varios(id),
	nReservado INT DEFAULT 0,
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[vigencias] (
	nombre VARCHAR(255),
	maximoproyectos INT,
	presupuesto MONEY,
	cierre DATETIME,
	nReservado INT DEFAULT 0,
	PRIMARY KEY(nombre)
);
CREATE TABLE dbo.[personas] (

	id BIGINT NOT NULL IDENTITY(1,1),
	nombre VARCHAR(255),
	tipop BIGINT REFERENCES varios(id),
	grupop BIGINT REFERENCES varios(id),
	direccion VARCHAR(255),
	telefono VARCHAR(50),
	celular VARCHAR(50),
	email VARCHAR(255),
	cargo VARCHAR(255),
	cedula VARCHAR(255),
	empresa VARCHAR(255),
	municipio BIGINT REFERENCES municipios(id),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[usuarios] (
	documento VARCHAR(20) NOT NULL,
	login	  VARCHAR(255),
	grupo	  BIGINT REFERENCES varios(id),
	nombre	  BIGINT REFERENCES personas(id),
	clave	  VARCHAR(255),
	usesion	  DATETIME,
	estado	  TINYINT,
	vigencias TEXT,
	PRIMARY KEY(documento)
);
CREATE TABLE dbo.[usuariosentes] ( 

	documento VARCHAR(20) NOT NULL,
	enteterritorial BIGINT REFERENCES eterritoriales(codigo)
);
CREATE TABLE dbo.[insteducativas] (
	id BIGINT IDENTITY(1,1),
	codigo VARCHAR(255),
	nombre TEXT NOT NULL,
	calendario BIGINT REFERENCES varios(id),
	tpoblacion BIGINT REFERENCES varios(id),
	sector BIGINT REFERENCES varios(id),
	municipio BIGINT REFERENCES municipios(id),
	modalidad BIGINT REFERENCES varios(id),
	rector BIGINT,
	zona BIGINT REFERENCES varios(id),
	barrioinspeccion VARCHAR(255),
	direccion VARCHAR(255),
	telefonos VARCHAR(255),
	fax VARCHAR(255),
	web VARCHAR(255),
	email VARCHAR(255),
	principal BIGINT REFERENCES insteducativas(id),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[institucionesvariacion] (
	id BIGINT NOT NULL IDENTITY(1,1),
	institucion BIGINT NOT NULL REFERENCES insteducativas(id),
	anolectivo INT,
	alumnos INT,
	jornadas INT,
	nReservado INT,
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[proyectosi] (

	id BIGINT NOT NULL IDENTITY(1,1),
	codigo VARCHAR(255),
	eterritorial BIGINT NOT NULL REFERENCES eterritoriales(codigo),
	municipio BIGINT NOT NULL REFERENCES municipios(id),
	institucion BIGINT NOT NULL REFERENCES insteducativas(id),
	jornadas INT,
	NoResolucion VARCHAR(255),
	FechaResolucion DATETIME,
	criticidad BIGINT REFERENCES varios(id),
	tipo BIGINT REFERENCES varios(id),
	estudiantesb INT,
	estudiantesd INT,
	vigencia VARCHAR(255) NOT NULL REFERENCES vigencias(nombre),
	observaciones VARCHAR(1024),
	priorizacion INT,
	estado BIGINT REFERENCES varios(id),
	observacionesa VARCHAR(1024),
	objeto VARCHAR(1024),
	conceptomen BIGINT REFERENCES varios(id),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[proyectosemergencia] (
	proyecto BIGINT REFERENCES proyectosi,
	numero VARCHAR(255),
	fecha DATETIME,
	PRIMARY KEY(proyecto)
);
CREATE TABLE dbo.[objetoinscrito] (
	id BIGINT NOT NULL IDENTITY(1,1),
	proyecto BIGINT NOT NULL REFERENCES proyectosi(id),
	concepto BIGINT NOT NULL REFERENCES varios(id),
	cantidad FLOAT,
	unidades BIGINT REFERENCES varios(id),
	valor MONEY,
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[presupuestoi] (
	id BIGINT NOT NULL IDENTITY(1,1),
	proyecto BIGINT NOT NULL REFERENCES proyectosi(id),
	porcentajeMEN FLOAT,
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[rectores] (
	id BIGINT NOT NULL IDENTITY(1,1),
	institucion BIGINT REFERENCES insteducativas(id),
	ano INT,
	nombre VARCHAR(255),
	direccion  VARCHAR(255),
	telefono VARCHAR(255),
	celular VARCHAR(255),
	fax VARCHAR(255),
	email VARCHAR(255),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[asignacionpresupuestal] (
	id BIGINT NOT NULL IDENTITY(1,1),
	eterritorial BIGINT REFERENCES eterritoriales(codigo),
	partida MONEY,
	fecha DATETIME,
	partidaf MONEY,
	fechaf DATETIME,
	grupo BIGINT REFERENCES varios(id),
	observaciones VARCHAR(1024),
	vigencia VARCHAR(255) REFERENCES vigencias(nombre),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[ampliacionmatricula] (
	id BIGINT NOT NULL IDENTITY(1,1),
	eterritorial BIGINT REFERENCES eterritoriales(codigo),
	vigencia VARCHAR(255),
	actual INT,
	concepto VARCHAR(1024),
	observaciones VARCHAR(1024),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[conveniosm] (
	id BIGINT NOT NULL IDENTITY(1,1),
	eterritorial BIGINT REFERENCES eterritoriales(codigo),
	codigo VARCHAR(255) NOT NULL,
	estado INT,
	tipo BIGINT REFERENCES varios(id),
	fechaaprobacion DATETIME,
	plazo INT,
	valor MONEY,
	rdp VARCHAR(255),
	fechardp DATETIME,
	cdp VARCHAR(255),
	fechacdp DATETIME,
	objeto VARCHAR(1024),
	observaciones VARCHAR(1024),
	vigencia VARCHAR(255) REFERENCES vigencias(nombre),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[documentosconveniom] (
	
	id BIGINT NOT NULL IDENTITY(1,1),
	convenio BIGINT NOT NULL REFERENCES conveniosm(id),
	documento BIGINT NOT NULL REFERENCES varios(id),
	fecha DATETIME,
	numero VARCHAR(255),
	observaciones VARCHAR(1024),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[variosconveniom] (
	convenio BIGINT NOT NULL REFERENCES conveniosm(id),
	tipo BIGINT REFERENCES varios(id),
	PRIMARY KEY(convenio)
);
CREATE TABLE dbo.[aportes] (
	id BIGINT NOT NULL IDENTITY(1,1),
	eterritorial BIGINT REFERENCES eterritoriales(codigo),
	certificado BIT,
	origen BIGINT REFERENCES varios(id),
	fecha DATETIME,
	valor MONEY,
	estado BIGINT REFERENCES varios(id),
	observaciones VARCHAR(1024),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[proyectos] (
	id BIGINT NOT NULL IDENTITY(1,1),
	proyectoi BIGINT REFERENCES proyectosi(id),
	codigo VARCHAR(255),
	eterritorial BIGINT NOT NULL REFERENCES eterritoriales(codigo),
	municipio BIGINT REFERENCES municipios(id),
	institucion BIGINT REFERENCES insteducativas(id),
	ultimarevision DATETIME,
	tipo BIGINT REFERENCES varios(id),
	conceptoviabilizacion BIGINT REFERENCES varios(id),
	estado BIGINT,
	vigencia VARCHAR(255) REFERENCES vigencias(nombre),
	fechaaprobacion DATETIME,
	observaciones VARCHAR(1024),
	objeto VARCHAR(1024),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[responsableviabilizacion] (
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	nit VARCHAR(255),
	nombre VARCHAR(255),
	representante VARCHAR(255),
	direccion VARCHAR(255),
	telefono VARCHAR(255),
	fax VARCHAR(255),
	email VARCHAR(255),
	PRIMARY KEY(proyecto)
);
CREATE TABLE dbo.[objeto] (
	id BIGINT NOT NULL IDENTITY(1,1),
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	concepto BIGINT NOT NULL REFERENCES varios(id),
	cantidad FLOAT,
	unidades BIGINT REFERENCES varios(id),
	valor MONEY,
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[presupuesto] (
	id BIGINT NOT NULL IDENTITY(1,1),
	proyecto BIGINT REFERENCES proyectos(id),
	valormen MONEY,
	porcentaje FLOAT,
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[documentoproyecto] (
	
	id BIGINT NOT NULL IDENTITY(1,1),
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	documento BIGINT NOT NULL REFERENCES varios(id),
	fecha DATETIME,
	numero VARCHAR(255),
	observaciones VARCHAR(1024),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[aspectostecnicos] (
	id BIGINT NOT NULL IDENTITY(1,1),
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	aspecto BIGINT NOT NULL REFERENCES varios(id),
	cantidad INT,
	fecha DATETIME,
	observaciones VARCHAR(1024),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[infraestructura] (
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	aulasn INT,
	aulasr INT,
	estudiantesp FLOAT,
	jornadas INT,
	banos INT,
	asanitarios INT,
	sillas INT,
	valord MONEY,
	PRIMARY KEY(proyecto)
);
CREATE TABLE dbo.[areas] (
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	aulas FLOAT,
	banos FLOAT,
	circulaciones FLOAT,
	indiceocupacion FLOAT,
	indiceconstruccion FLOAT,
	PRIMARY KEY(proyecto)
);
CREATE TABLE dbo.[valor] (
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	aulas MONEY,
	banos MONEY,
	circulaciones MONEY,
	m2aula MONEY,
	m2banos  MONEY,
	m2circulacion MONEY,
	PRIMARY KEY(proyecto)
);
CREATE TABLE dbo.[observacionesproyecto] (
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	documentos VARCHAR(1024),
	tecnicas VARCHAR(1024),
	infraestructura VARCHAR(1024),
	valores VARCHAR(1024),
	PRIMARY KEY(proyecto)
);
CREATE TABLE dbo.[adicionesconvenio] (
	id BIGINT NOT NULL IDENTITY(1,1),
	convenio BIGINT NOT NULL REFERENCES conveniosm(id),
	valor MONEY,
	plazo INT,
	fecha DATETIME,
	cdp VARCHAR(255),
	fechacdp DATETIME,
	rdp VARCHAR(255),
	fechardp DATETIME,
	estado BIGINT REFERENCES varios(id),
	observaciones VARCHAR(1024),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[desembolsosconvenio] (

	id BIGINT NOT NULL IDENTITY(1,1),
	convenio BIGINT NOT NULL REFERENCES conveniosm(id),
	numero INT,
	valor MONEY,
	fecha DATETIME,
	comprobante VARCHAR(255),
	fechacomprobante DATETIME,
	observaciones VARCHAR(1024),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[rendimientosconvenio] (
	id BIGINT NOT NULL IDENTITY(1,1),
	convenio BIGINT NOT NULL REFERENCES conveniosm(id),
	valor MONEY,
	mes INT,
	ano INT,
	fecha DATETIME,
	estado BIGINT REFERENCES varios(id),
	observaciones VARCHAR(1024),
	PRIMARY KEY(id)	
);
CREATE TABLE dbo.[liquidacionconvenio] (
	convenio BIGINT NOT NULL REFERENCES conveniosm(id),
	informe BIT,
	fechainforme DATETIME,
	documentos BIT,
	actaliquidacion BIT,
	fechaliquidacion DATETIME,
	observaciones VARCHAR(1024),
	PRIMARY KEY(convenio)
);
CREATE TABLE dbo.[contratos] (
	id BIGINT NOT NULL IDENTITY(1,1),
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	codigo VARCHAR(255),
	fechas DATETIME,
	fechainicio DATETIME,
	clase BIGINT REFERENCES varios(id),
	objeto VARCHAR(1024),
	plazo INT,
	valor MONEY,
	contratista VARCHAR(255),
	rlegal VARCHAR(255),
	historial VARCHAR(1024),	
	estado BIGINT REFERENCES varios(id),
	observaciones VARCHAR(1204),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[reporte] (
	id BIGINT NOT NULL IDENTITY(1,1),
	contrato BIGINT NOT NULL REFERENCES contratos(id),
	valoravance MONEY,
	fecha DATETIME,
	tipo BIGINT REFERENCES varios(id),
	interventor VARCHAR(255),
	cargo VARCHAR(255),
	observaciones VARCHAR(1024),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[saldosp] (
 	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
 	estado BIGINT REFERENCES varios(id),
 	observaciones VARCHAR(1024),
 	PRIMARY KEY(proyecto)
);
CREATE TABLE dbo.[documentosproyectof] (
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	informef DATETIME,
	actat DATETIME,
	cinterventoria DATETIME,
	chacienda DATETIME,
	cmunicipal DATETIME,
	observaciones VARCHAR(1024),
	PRIMARY KEY(proyecto)
);
CREATE TABLE dbo.[usuarios2] (
	id BIGINT NOT NULL IDENTITY(1,1),
	login	  VARCHAR(255),
	nombre	  VARCHAR(255),
	grupo	BIGINT NOT NULL REFERENCES varios(id),
	clave	  VARCHAR(255),
	usesion	  DATETIME,
	estado	  INT,
	vigencias VARCHAR(1024),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[usuariosentes2] ( 

	usuario BIGINT NOT NULL REFERENCES usuarios2(id),
	enteterritorial BIGINT REFERENCES eterritoriales(codigo),
	vigencia VARCHAR(255) REFERENCES vigencias(nombre),
);
CREATE TABLE dbo.[derivados] (
	id BIGINT NOT NULL IDENTITY(1,1),
	codigo VARCHAR(255),
	eterritorial BIGINT REFERENCES eterritoriales(codigo),
	proyecto BIGINT REFERENCES proyectos(id),
	aprobacion DATETIME,
	plazo INT,
	estado BIGINT REFERENCES varios(id),
	valor MONEY,
	rpe	VARCHAR(255),
	rpm VARCHAR(255),
	observaciones VARCHAR(1024),
	vigencia VARCHAR(255) REFERENCES vigencias(nombre),
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[fotos] (
	id BIGINT NOT NULL IDENTITY(1,1),
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	fecha DATETIME,
	foto IMAGE,
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[fotosv] (
	id BIGINT NOT NULL IDENTITY(1,1),
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	fecha DATETIME,
	foto IMAGE,
	PRIMARY KEY(id)
);
CREATE TABLE dbo.[revisorvia] (
	proyecto BIGINT NOT NULL REFERENCES proyectos(id),
	nombre VARCHAR(255),
	fecha DATETIME,
	PRIMARY KEY(proyecto)
);
IF COL_LENGTH('dbo.usuarios2','usesion') IS NULL ALTER TABLE dbo.[usuarios2] ADD [usesion] datetime NULL;
IF COL_LENGTH('dbo.usuarios2','estado') IS NULL ALTER TABLE dbo.[usuarios2] ADD [estado] int NULL;
IF COL_LENGTH('dbo.proyectosi','estudiantesb') IS NULL ALTER TABLE dbo.[proyectosi] ADD [estudiantesb] int NULL;
IF COL_LENGTH('dbo.proyectosi','estudiantesd') IS NULL ALTER TABLE dbo.[proyectosi] ADD [estudiantesd] int NULL;
IF COL_LENGTH('dbo.proyectosi','FechaResolucion') IS NULL ALTER TABLE dbo.[proyectosi] ADD [FechaResolucion] datetime NULL;
IF COL_LENGTH('dbo.proyectosi','departamento') IS NULL ALTER TABLE dbo.[proyectosi] ADD [departamento] bigint NULL;
IF COL_LENGTH('dbo.menus','orden') IS NULL ALTER TABLE dbo.[menus] ADD [orden] int NULL;
IF COL_LENGTH('dbo.departamentos','color') IS NULL ALTER TABLE dbo.[departamentos] ADD [color] int NULL;
IF COL_LENGTH('dbo.departamentos','observaciones') IS NULL ALTER TABLE dbo.[departamentos] ADD [observaciones] nvarchar(250) NULL;
IF COL_LENGTH('dbo.insteducativas','rector') IS NULL ALTER TABLE dbo.[insteducativas] ADD [rector] nvarchar(100) NULL;
GO
-- Fictional SQL account
USE master;
CREATE LOGIN portal_demo_user WITH PASSWORD='$(FictionalSqlPassword)';
USE portal_demo;
CREATE USER portal_demo_user FOR LOGIN portal_demo_user;
GO
-- Fictional registration and report data
USE portal_demo;
INSERT INTO dbo.departamentos ([codigo], [nombre], [mapa], [color], [observaciones]) VALUES (901, N'Departamento Ejemplo', 'demo901', 0, N'Registro ficticio');
SET IDENTITY_INSERT dbo.varios ON;
INSERT INTO dbo.varios ([id], [nombre], [tipo]) VALUES (1, 'Región Ejemplo', 1),
(2, 'Grupo Ejemplo', 2);
SET IDENTITY_INSERT dbo.varios OFF;
INSERT INTO dbo.vigencias ([nombre]) VALUES ('2008');
SET IDENTITY_INSERT dbo.usuarios2 ON;
INSERT INTO dbo.usuarios2 ([id], [login], [nombre], [grupo], [clave], [vigencias], [estado]) VALUES (501, 'alicia_demo', N'Alicia Ejemplo', 2, '$(FictionalPortalPassword)', '2008', 1);
SET IDENTITY_INSERT dbo.usuarios2 OFF;
INSERT INTO dbo.eterritoriales ([codigo], [nombre], [region], [nReservado]) VALUES (901, N'Entidad Ejemplo', 1, 0);
INSERT INTO dbo.usuariosentes2 ([usuario], [enteterritorial], [vigencia]) VALUES (501, 901, '2008');
SET IDENTITY_INSERT dbo.menus ON;
INSERT INTO dbo.menus ([id], [nombre], [descripcion], [url], [padre], [orden]) VALUES (902, N'Catálogo de prueba', N'Catálogo de prueba', '/datosmapa.aspx', NULL, 902),
(903, N'Inscripción ficticia', N'Inscripción ficticia', '/inscripcion/inscripcion/inscripcion.aspx', NULL, 903),
(904, N'Informe ficticio', N'Informe ficticio', '/informes/institucion/Default.aspx', NULL, 904);
SET IDENTITY_INSERT dbo.menus OFF;
SET IDENTITY_INSERT dbo.gruposupermisos ON;
INSERT INTO dbo.gruposupermisos ([id], [grupo], [menu], [permisos]) VALUES (902, 2, 902, 7),
(903, 2, 903, 7),
(904, 2, 904, 7);
SET IDENTITY_INSERT dbo.gruposupermisos OFF;
SET IDENTITY_INSERT dbo.municipios ON;
INSERT INTO dbo.municipios ([id], [codigo], [nombre], [departamento], [eterritorial]) VALUES (901, 901, N'Municipio Ejemplo', 901, 901);
SET IDENTITY_INSERT dbo.municipios OFF;
SET IDENTITY_INSERT dbo.insteducativas ON;
INSERT INTO dbo.insteducativas ([id], [codigo], [nombre], [municipio]) VALUES (701, 'INST-EJEMPLO-701', 'Institución Ejemplo', 901);
SET IDENTITY_INSERT dbo.insteducativas OFF;
SET IDENTITY_INSERT dbo.proyectosi ON;
INSERT INTO dbo.proyectosi ([id], [codigo], [eterritorial], [municipio], [institucion], [vigencia], [observaciones]) VALUES (701, 'SOL-EJEMPLO-701', 901, 901, 701, '2008', N'Solicitud Ejemplo');
SET IDENTITY_INSERT dbo.proyectosi OFF;
SET IDENTITY_INSERT dbo.conveniosm ON;
INSERT INTO dbo.conveniosm ([id], [codigo], [eterritorial], [vigencia], [observaciones]) VALUES (705, 'CONV-EJEMPLO-705', 901, '2008', N'Convenio Ejemplo');
SET IDENTITY_INSERT dbo.conveniosm OFF;
GRANT SELECT, INSERT, UPDATE ON SCHEMA::dbo TO portal_demo_user;
GO
