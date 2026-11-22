# Portal de inscripciones e informes educativos
[English](#english) · [Español](#español)

## English


Archived ASP.NET portal for recording education applications and producing institutional reports

> [!WARNING]
> **Status: archived.** Source from 2006–2008. This edition precompiled with .NET Framework 4.8.

### Overview

The portal stores forms and reference lists for education applications. Operators enter records. Reviewers use the application and reporting pages to follow decisions and consult institutions. The available screens vary by edition.

### Features

- Enter a school’s application and check its institution in [Inscripción](inscripcion/inscripcion/inscripcion.aspx).
- Review project applications and record the approval decision in [Aprobación](aprobacion/proyectos/aprobacion.aspx).
- Review the feasibility information for a project in [Viabilización](viabilizacion/proyectos/viabilizacion.aspx).
- Look up a project for follow-up in [Seguimiento](seguimiento/proyectos/proyecto.aspx).
- Open the project photo gallery in [Galería](fotos/proyectos/galeria.aspx).

### Requirements

The 2026 reconstruction uses `OrganizationExample22` as a neutral name for the historical database, virtual path and solution. The isolated test uses the included fictional fixture.

| Component | Version | Purpose |
| --- | --- | --- |
| ASP.NET precompiler | 4.8.10001.0 | Compiles the Web Forms pages |
| IIS Express | 10.0.26013.1000 | Runs the verification site |
| SQL Server Developer | 2022 | Stores the fictional catalogue |
| Anthem.NET | 1.3.1.0 | Supports the original page controls |

### Getting started

Run `docker compose -f demo/compose.yaml up -d --pull never` with `PORTAL_DEMO_SA_PASSWORD` set to a newly generated test password in your terminal. The SQL Server 2022 Developer image must already be available locally. Use `127.0.0.1,11433` in the generated configuration. Change the loopback port with `PORTAL_DEMO_PORT` if needed. Run `docker compose -f demo/compose.yaml down -v` to delete the demo database when finished.

From a Windows checkout, run `pwsh -NoProfile -File demo/Build.ps1 -Output <new-folder>`. Serve the resulting `site` folder with IIS Express on `127.0.0.1`. Load [the fictional schema](demo/schema.sql) into a disposable SQL Server instance bound to `127.0.0.1`. Add `servidor`, `db`, `usuario` and `contrasena` to `appSettings` in the generated site configuration. Point them to that instance, the `portal_demo` database and a new test user.

### Usage

To try the registration and institution report filters, start a fresh local SQL Server container. Run [the extended fictional data script](demo/PortalFlows.sql) with `sqlcmd` and set `FictionalSqlPassword` and `FictionalPortalPassword` to two new local test values. Use the first in `contrasena` in the generated site configuration. Sign in as `alicia_demo` with the second. In the registration form, verify institution `INST-EJEMPLO-701`. In the institution report, search using the same code.

To reproduce sign-in, map a fictional SQL login to a database user named `portal_demo_user`. Run [the sign-in fixture](demo/PortalLogin.sql) with `sqlcmd` and the variable `FictionalPortalPassword` set to a new fictional password. Open `Default.aspx` and sign in as `alicia_demo` using that password. The application records the sign-in and opens an entry menu with the fictional catalogue link.

Copy `demo/CatalogProbe.aspx.example` into the test site as `prueba-catalogo.aspx`. Open that page in the local site. The fictional record is Departamento Ejemplo. The initializer opens the original `datosmapa.aspx` page and that page returns the record as XML.

### Project structure

- `estilos/` · Stylesheets and small image assets used by the restored pages.
- `aplicativo/`. Institutional catalogues. Open the school list in `aplicativo/institucion/ieducativas.aspx`.
- `App_Code/`. Shared record and data access classes. Start with `App_Code/CMapa.cs`.
- `aprobacion/`. Agreement and project decisions, including proyectos/aprobacion.aspx.
- `ayuda/`. Common help layout in ayuda.master.
- `bin/` · Original Anthem.NET assembly
- `demo/` · Build and fictional SQL scenario
- `ediciones/` · Separate builds of the recovered March 2006 and August 2008 sites
- `demos/`. Example catalogue query in ejemplo.json.
- `fotos/`. Project photos and gallery entry points under proyectos/.
- `imagenes/`. Shared page headers, background images and navigation icons.
- `importar/`. File import handler in archivos/Default.aspx.cs.
- `informes/`. Open `informes/institucion/Default.aspx` to find an institution. This archive retains code for the general report, whose page markup is missing.
- `inscripcion/`. Open `inscripcion/inscripcion/inscripcion.aspx` to start a new application or find an existing institution.
- `menu/`. JSCookMenu.js and the visual theme for the menu.
- `scripts/`. Common browser helpers, including comun.js.
- `seguimiento/`. Follow-up pages for projects and agreements, including proyectos/proyecto.aspx.
- `sistema/`. Administrative catalogues and user groups. Start at `sistema/usuarios/grupos.aspx`.
- `viabilizacion/`. Project feasibility entry in proyectos/viabilizacion.aspx.
- `docs/` · Evidence from the fictional verification

### Verification

- Two original revisions were preserved. March 2006 stops at a duplicate control. August 2008 has precompiled page markup without its matching assembly. The 2026 follow-up carries separately recovered editions that build and return a fictional lookup result.
- Of the 29 cuts before this revision, 27 build. The database adapter read a fictional department in nine, and the original map page returned fictional XML in 18. The 2026 follow-up below is the cut with evidence for sign-in, menu, submission and report filters.
- The 2026 portal signed in against disposable SQL Server with fictional data, saved an application and filtered the institution and agreement reports. Both project report pages returned HTTP 200 and opened their print views. `segproy.aspx` keeps the provisional query recovered in the backup. `segconproy.aspx` opened but the test did not confirm a fictional result row. Both data limits are part of the approved scope.

The original March 2006 revision has duplicate `m_bAnterior` controls and the August 2008 revision includes precompiled markup without the matching assembly. The 2026 recovery adds buildable copies in [March 2006](ediciones/2006-03/) and [August 2008](ediciones/2008-08/), using matching source pages and dependencies from the following archived editions. The dated 2006–2008 entries preserve the original application logic and dates. The authorized 2026 reconstruction neutralizes institutional wording, contact details and emblems. The local database test uses fictional data. The final release also exercises login, the menu, registration lookup, the institution report filter and the XML catalog. The new fixture enables the application form, supplies fictional priority and type options, and supports a local submission trial. In the 23 November 2026 repeat the form saved a new fictional application, and the test confirmed its row directly in SQL Server. The archived crest is omitted from the demo. The report view rendered fictional agreement status and tracking data. The project overview uses an archived placeholder query. The archived project-by-agreement report did not display a matching fictional row in this test.

Docker Compose 5.6.1 starts the disposable service in this verification.

Windows build 10.0.26300, IIS Express 10.0.26013.1000 (WinBuild.160101.0800), SQL Server 16.0.4325.4 and .NET Framework ASP.NET Compiler 4.8.10001.0.

The November 25 local verification built the main site and the two recovered editions. The map and pager checks used an isolated fictional SQL Server catalog. A separate test of the main site signed in, opened the reports and saved a fictional application. The portal was built in Windows and tested with IIS Express and a disposable SQL Server instance. The catalogue query and sign-in passed. SQL Server recorded the sign-in and the menu showed the fictional link. The registration institution lookup and institution report search returned the fictional row. SQL Server stored a new fictional application submitted from the form. The agreement reports rendered sample data. Both archived project reports opened their print view, but the test did not confirm their data output.

![The original XML page contains Departamento Ejemplo](docs/media/xml-catalogue.png)

### Screenshots

The stills show the live application on November 22, 2026. The November 23 recording enters the fictional institution code and shows the single matching result.

![Institution report lookup from blank form to one fictional row](docs/media/institution-search.gif)

The local trial shows the fictional school in the application form and search results. SQL Server confirmed the fictional application was saved.

![Application form with a fictional school](docs/media/solicitud-demo.png)

![Search result containing the fictional school](docs/media/institucion-demo.png)

### History

The archive follows portal development from 2006 to 2008, including applications, agreements and reports. The 2026 work adds the bilingual build guide, two separately recovered editions and a fictional local trial.

### Support

Use an issue with the commit and the fictional reproduction steps.

### Contributing

We welcome reproducibility fixes. Include the edition, exact build command, tools and a fictional example when proposing a change.

### License

The original application is available under the [MIT license](LICENSE).

---

## Español


Archivado — portal ASP.NET para registrar inscripciones educativas y preparar informes de instituciones

> [!WARNING]
> **Estado: archivado.** Código de 2006 a 2008. Esta edición precompiló con .NET Framework 4.8.

### Descripción general

El portal reúne formularios y catálogos para gestionar inscripciones educativas. El personal registra solicitudes y usa las páginas de revisión e informes para seguir decisiones y consultar instituciones. Las pantallas disponibles dependen de la edición.

### Funciones

- Registra la solicitud y verifica su institución en [Inscripción](inscripcion/inscripcion/inscripcion.aspx).
- Revisa las solicitudes y registra la aprobación en [Aprobación](aprobacion/proyectos/aprobacion.aspx).
- Revisa la viabilidad de un proyecto en [Viabilización](viabilizacion/proyectos/viabilizacion.aspx).
- Consulta un proyecto para hacer seguimiento en [Seguimiento](seguimiento/proyectos/proyecto.aspx).
- Abre la galería fotográfica del proyecto en [Galería](fotos/proyectos/galeria.aspx).

### Requisitos

La reconstrucción de 2026 usa `OrganizationExample22` como nombre neutro para la base, la ruta virtual y la solución archivadas. La prueba aislada usa los datos ficticios incluidos.

| Componente | Versión | Función |
| --- | --- | --- |
| Precompilador ASP.NET | 4.8.10001.0 | Compila las páginas Web Forms |
| IIS Express | 10.0.26013.1000 | Ejecuta el sitio de prueba |
| SQL Server Developer | 2022 | Guarda el catálogo ficticio |
| Anthem.NET | 1.3.1.0 | Permite usar los controles originales de las páginas |

### Cómo empezar

Ejecuta `docker compose -f demo/compose.yaml up -d --pull never` con `PORTAL_DEMO_SA_PASSWORD` definida en tu terminal con una contraseña nueva de prueba. La imagen SQL Server 2022 Developer debe estar disponible localmente. Usa `127.0.0.1,11433` en la configuración generada. Si necesitas otro puerto local, defínelo en `PORTAL_DEMO_PORT`. Al terminar, ejecuta `docker compose -f demo/compose.yaml down -v` para borrar la base de prueba.

Desde una copia en Windows, ejecuta `pwsh -NoProfile -File demo/Build.ps1 -Output <new-folder>`. Abre la carpeta `site` resultante con IIS Express en `127.0.0.1`. Carga [el esquema ficticio](demo/schema.sql) en una instancia desechable de SQL Server enlazada a `127.0.0.1`. Agrega `servidor`, `db`, `usuario` y `contrasena` a `appSettings` en la configuración generada del sitio. Indica esa instancia, la base `portal_demo` y un usuario nuevo de prueba.

### Uso

Para probar los filtros de inscripción e informes de instituciones, inicia un SQL Server local nuevo. Ejecuta [el escenario ficticio ampliado](demo/PortalFlows.sql) con `sqlcmd` y define `FictionalSqlPassword` y `FictionalPortalPassword` con dos valores nuevos de prueba. Usa el primero en `contrasena` en la configuración generada. Ingresa como `alicia_demo` con el segundo. En el formulario de inscripción, verifica la institución `INST-EJEMPLO-701`. En el informe, busca con el mismo código.

Para reproducir el ingreso, asigna un login ficticio de SQL a un usuario de la base llamado `portal_demo_user`. Ejecuta [el escenario de ingreso](demo/PortalLogin.sql) con `sqlcmd` y define la variable `FictionalPortalPassword` con una contraseña ficticia nueva. Abre `Default.aspx` e ingresa como `alicia_demo` con esa contraseña. La aplicación registra el ingreso y abre el menú de entrada con el enlace al catálogo ficticio.

Copia `demo/CatalogProbe.aspx.example` al sitio de prueba como `prueba-catalogo.aspx`. Abre esa página en el sitio local. El registro ficticio se llama Departamento Ejemplo. El inicializador abre la página original `datosmapa.aspx` y esa página devuelve el registro en XML.

### Estructura del proyecto

- `estilos/` · Hojas de estilo y pequeños recursos gráficos de las páginas recuperadas.
- `aplicativo/`. Catálogos institucionales. Abre la lista de escuelas en `aplicativo/institucion/ieducativas.aspx`.
- `App_Code/`. Clases compartidas y de acceso a datos. Consulta `App_Code/CMapa.cs`.
- `aprobacion/`. Decisiones de convenios y proyectos, incluida proyectos/aprobacion.aspx.
- `ayuda/`. Plantilla compartida de ayuda en ayuda.master.
- `bin/` · Ensamblado original de Anthem.NET
- `demo/` · Compilación y escenario ficticio en SQL
- `ediciones/` · Compilaciones independientes de las ediciones recuperadas de marzo de 2006 y agosto de 2008
- `demos/`. Consulta de catálogo de ejemplo en ejemplo.json.
- `fotos/`. Fotos y páginas de galería de proyectos en proyectos/.
- `imagenes/`. Encabezados compartidos, fondos e iconos de navegación.
- `importar/`. Manejador de importación en archivos/Default.aspx.cs.
- `informes/`. Abre `informes/institucion/Default.aspx` para buscar una institución. El respaldo conserva el código del informe general. Falta el marcado de la página.
- `inscripcion/`. Abre `inscripcion/inscripcion/inscripcion.aspx` para registrar una solicitud o buscar una institución.
- `menu/`. JSCookMenu.js y el tema visual del menú.
- `scripts/`. Funciones de navegador compartidas, incluida comun.js.
- `seguimiento/`. Seguimiento de proyectos y convenios, incluido proyectos/proyecto.aspx.
- `sistema/`. Catálogos administrativos y grupos de usuarios. Empieza en `sistema/usuarios/grupos.aspx`.
- `viabilizacion/`. Ficha de viabilidad de proyectos en proyectos/viabilizacion.aspx.
- `docs/` · Evidencia de la comprobación ficticia

### Verificación

- Se conservaron dos revisiones originales. Marzo de 2006 se detiene por un control duplicado. Agosto de 2008 trae páginas precompiladas sin el ensamblado correspondiente. El seguimiento de 2026 incluye ediciones recuperadas por separado que compilan y devuelven el resultado de una consulta ficticia.
- De los 29 cortes anteriores a esta revisión, 27 compilan. El adaptador consultó un departamento ficticio en nueve y la página de mapa original devolvió XML ficticio en 18. El seguimiento de 2026 que se describe abajo tiene sus propias pruebas de ingreso, menú, inscripción y filtros de informes.
- La prueba del portal de 2026 inició sesión con datos ficticios en SQL Server desechable, guardó una solicitud y filtró los informes de instituciones y convenios. Los dos informes de proyectos devolvieron HTTP 200 y abrieron la vista de impresión. `segproy.aspx` conserva la consulta provisional recuperada del respaldo. En `segconproy.aspx` se abrió la vista y quedó sin confirmar una fila de resultado ficticia. Los dos límites de datos forman parte del alcance aprobado.

La versión original de marzo de 2006 tiene controles `m_bAnterior` duplicados y la de agosto de 2008 incluye marcado precompilado sin su ensamblado. La recuperación de 2026 añade copias compilables en [marzo de 2006](ediciones/2006-03/) y [agosto de 2008](ediciones/2008-08/) con las páginas de código y dependencias que se conservan en las ediciones siguientes. Las entradas de 2006 a 2008 conservan las fechas y la lógica original de la aplicación. La reconstrucción autorizada de 2026 neutraliza rótulos institucionales, datos de contacto y escudos. La prueba local usa datos ficticios. La última versión también comprueba ingreso, menú, búsqueda de inscripción, filtro del informe de instituciones y catálogo XML. El escenario nuevo habilita el formulario y agrega opciones ficticias de prioridad y tipo para ensayar el envío local. El 23 de noviembre de 2026 el formulario guardó una nueva solicitud ficticia y se confirmó su fila en SQL Server. La demo omite el escudo archivado. La vista de informes mostró datos ficticios de estado y seguimiento de convenios. El informe general de proyectos conserva una consulta provisional de la época. El informe de proyectos por convenio no mostró una fila ficticia en este ensayo.

Docker Compose 5.6.1 inicia el servicio desechable de esta verificación.

Windows compilación 10.0.26300, IIS Express 10.0.26013.1000 (WinBuild.160101.0800), SQL Server 16.0.4325.4 y compilador ASP.NET de .NET Framework 4.8.10001.0.

La verificación local del 25 de noviembre compiló el sitio principal y las dos ediciones recuperadas. Las pruebas de mapa y paginación usaron un catálogo ficticio aislado en SQL Server. Una prueba aparte del sitio principal inició sesión, abrió los informes y guardó una solicitud ficticia. El portal se compiló en Windows y se probó con IIS Express y una instancia desechable de SQL Server. Pasaron la consulta del catálogo y el ingreso. SQL Server registró el ingreso y el menú mostró el enlace ficticio. La búsqueda de la institución en la inscripción y el filtro del informe devolvieron el registro ficticio. SQL Server guardó una solicitud ficticia enviada desde el formulario. Los informes de convenios mostraron datos de ejemplo. Los dos informes de proyectos abrieron la vista de impresión y quedó sin confirmar la salida de sus datos.

![La página XML original contiene Departamento Ejemplo](docs/media/xml-catalogue.png)

### Capturas

Las imágenes muestran la aplicación real del 22 de noviembre de 2026. La grabación del 23 de noviembre introduce el código ficticio de institución y muestra la única fila encontrada.

![Búsqueda real de institución desde el formulario vacío hasta la fila ficticia](docs/media/institution-search.gif)

Las pantallas del ensayo muestran la institución ficticia en el formulario y en los resultados de búsqueda. SQL Server confirmó el guardado de una solicitud ficticia.

![Formulario de inscripción con institución ficticia](docs/media/solicitud-demo.png)

![Resultado de búsqueda con institución ficticia](docs/media/institucion-demo.png)

### Historia

El archivo sigue el desarrollo del portal de 2006 a 2008, con inscripción, convenios e informes. En 2026 se agregaron la guía bilingüe, dos ediciones recuperadas por separado y una prueba local ficticia.

### Soporte

Abre una incidencia con el commit y el procedimiento con datos ficticios.

### Contribuir

Se reciben correcciones que ayuden a reproducir las pruebas. Al proponer un cambio, incluye la edición, el comando exacto de compilación, las herramientas y un ejemplo ficticio.

### Licencia

La aplicación original se distribuye con [licencia MIT](LICENSE).
