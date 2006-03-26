# Portal de inscripciones e informes educativos
[English](#english) · [Español](#español)

## English


Archived ASP.NET portal for recording education applications and producing institutional reports

> [!WARNING]
> **Status: archived.** Source from 2006–2008. The compiler reports duplicate identifier `m_bAnterior` in the surviving markup.

### Overview

The portal stores forms and reference lists for education applications. Operators enter records. Reviewers use the application and reporting pages to follow decisions and consult institutions. The available screens vary by edition.

### Features

- Enter a school’s application and check its institution in [Inscripción](inscripcion/inscripcion/inscripcion.aspx).
- Review project applications and record the approval decision in [Aprobación](aprobacion/proyectos/aprobacion.aspx).
- Review the feasibility information for a project in [Viabilización](viabilizacion/proyectos/viabilizacion.aspx).
- Look up a project for follow-up in [Seguimiento](seguimiento/proyectos/proyecto.aspx).

### Requirements

| Component | Version | Purpose |
| --- | --- | --- |
| ASP.NET precompiler | 4.8.10001.0 | Compiles the Web Forms pages |
| IIS Express | 10.0.26013.1000 | Runs the verification site |
| SQL Server Developer | 2022 | Stores the fictional catalogue |
| Anthem.NET | Archived assembly | Supports the original page controls |

### Getting started

The compiler reports duplicate identifier `m_bAnterior` in the surviving markup.

### Usage

The archived source and its build diagnostic remain useful for reviewing the incomplete edition.

### Project structure

- `aplicativo/`. Institutional catalogues. Open the school list in `aplicativo/institucion/ieducativas.aspx`.
- `App_Code/`. Shared record and data access classes. Start with `App_Code/CConector.cs`.
- `aprobacion/`. Agreement and project decisions, including proyectos/aprobacion.aspx.
- `inscripcion/` · Application forms
- `menu/`. JSCookMenu.js and the visual theme for the menu.
- `seguimiento/`. Follow-up pages for projects and agreements, including proyectos/proyecto.aspx.
- `sistema/` · Administrative catalogues
- `viabilizacion/`. Project feasibility entry in proyectos/viabilizacion.aspx.

### Verification

The compiler reports duplicate identifier `m_bAnterior` in the surviving markup.

### History

The source edition keeps its recorded date of 2006-03-26. The 2026 reconstruction includes recovered components, setup instructions and local checks.

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
> **Estado: archivado.** Código de 2006 a 2008. El compilador informa que el identificador `m_bAnterior` está duplicado en el marcado conservado.

### Descripción general

El portal reúne formularios y catálogos para gestionar inscripciones educativas. El personal registra solicitudes y usa las páginas de revisión e informes para seguir decisiones y consultar instituciones. Las pantallas disponibles dependen de la edición.

### Funciones

- Registra la solicitud y verifica su institución en [Inscripción](inscripcion/inscripcion/inscripcion.aspx).
- Revisa las solicitudes y registra la aprobación en [Aprobación](aprobacion/proyectos/aprobacion.aspx).
- Revisa la viabilidad de un proyecto en [Viabilización](viabilizacion/proyectos/viabilizacion.aspx).
- Consulta un proyecto para hacer seguimiento en [Seguimiento](seguimiento/proyectos/proyecto.aspx).

### Requisitos

| Componente | Versión | Función |
| --- | --- | --- |
| Precompilador ASP.NET | 4.8.10001.0 | Compila las páginas Web Forms |
| IIS Express | 10.0.26013.1000 | Ejecuta el sitio de prueba |
| SQL Server Developer | 2022 | Guarda el catálogo ficticio |
| Anthem.NET | Ensamblado del respaldo | Permite usar los controles originales de las páginas |

### Cómo empezar

El compilador informa que el identificador `m_bAnterior` está duplicado en el marcado conservado.

### Uso

El código de archivo y su diagnóstico de compilación permiten revisar esta edición incompleta.

### Estructura del proyecto

- `aplicativo/`. Catálogos institucionales. Abre la lista de escuelas en `aplicativo/institucion/ieducativas.aspx`.
- `App_Code/`. Clases compartidas y de acceso a datos. Consulta `App_Code/CConector.cs`.
- `aprobacion/`. Decisiones de convenios y proyectos, incluida proyectos/aprobacion.aspx.
- `inscripcion/` · Formularios de inscripción
- `menu/`. JSCookMenu.js y el tema visual del menú.
- `seguimiento/`. Seguimiento de proyectos y convenios, incluido proyectos/proyecto.aspx.
- `sistema/` · Catálogos de administración
- `viabilizacion/`. Ficha de viabilidad de proyectos en proyectos/viabilizacion.aspx.

### Verificación

El compilador informa que el identificador `m_bAnterior` está duplicado en el marcado conservado.

### Historia

La edición conserva su fecha registrada del 2006-03-26. La reconstrucción de 2026 incorpora componentes recuperados, instrucciones y comprobaciones locales.

### Soporte

Abre una incidencia con el commit y el procedimiento con datos ficticios.

### Contribuir

El archivo conserva las ediciones de código como referencia.

### Licencia

La aplicación original se distribuye con [licencia MIT](LICENSE).
