# BookRadarMvc
Proyecto ASP.NET Core 8 MVC para buscar libros por autor usando una API externa y guardar historial de búsquedas.

## Estructura del proyecto
BookRadar/
├── wwwroot/ # Archivos estáticos (CSS, JS, imágenes)
├── Controllers/ # Controladores C#
│ ├── HomeController.cs
│ └── LibrosController.cs
├── Data/ # Acceso a datos
│ ├── Repositories/
│ │ ├── BookRepository.cs
│ │ └── IBookRepository.cs
│ └── AppDbContext.cs
├── Migrations/ # Migraciones EF Core
│ ├── 20250809014501_InitialCreate.cs
│ └── AppDbContextModelSnapshot.cs
├── Models/ # Modelos de datos
│ ├── BusquedaHistorial.cs
│ ├── ErrorViewModel.cs
│ └── LibroViewModel.cs
├── Services/ # Lógica de negocio
│ ├── BookService.cs
│ ├── IBookService.cs
│ ├── IOpenLibraryService.cs
│ └── OpenLibraryService.cs
├── Views/ # Vistas Razor
│ ├── Libros/
│ │ ├── Index.cshtml # Vista para búsqueda de libros
│ │ └── Historial.cshtml # Vista para historial de búsquedas
│ └── Home/
│ └── Privacy.cshtml # Vista de política de privacidad
└── Program.cs # Configuración principal de la app

## Pasos para ejecutar el proyecto

1. **Instalar .NET 8 SDK**  
   Descarga e instala desde:  
   https://dotnet.microsoft.com/en-us/download/dotnet/8.0

2. **Clonar el repositorio**  
   ```bash
   git clone https://tu-repositorio.git](https://github.com/btorresdominguez/BookRadarMvc
   cd BookRadar
3.  ## Restaurar paquetes
   dotnet restore

4. ## Configurar base de datos (si aplica)
 Revisa appsettings.json para la cadena de conexión.
 Ejecuta migraciones para crear la base: 
 dotnet ef database update
 
5. ## Ejecutar la aplicación
   dotnet run
   Navega a: https://localhost:5001

   ## Decisiones de diseño

- **Arquitectura MVC:** Separación clara entre lógica (Controllers), datos (Models) y presentación (Views) para mantener código organizado y fácil de mantener.
- **UI sencilla y funcional:**  
  - Formularios simples para búsqueda, resultados listados sin distracciones.  
  - Feedback claro con mensajes de error y confirmación usando `ViewBag`.  
  - Sin estilos complejos, para centrarse en funcionalidad; diseño básico que puede mejorarse con CSS/Bootstrap.
- **Spinner para mejor experiencia de usuario:**  
  - Se añadió un indicador de carga (*spinner*) para mostrar al usuario que la búsqueda está en proceso, evitando confusión y mejorando la     percepción de velocidad.
- **Enmaquetado Razor:** Uso de vistas Razor para integración fluida entre C# y HTML.
- **Colores y UX:**  
  - Diseño neutro y minimalista para mejor legibilidad.  
  - Pensado para usuarios que quieren rapidez y simplicidad al buscar libros.
- **Servicios y repositorios:** Lógica de negocio y acceso a datos desacoplados para facilitar pruebas y mantenibilidad.

- ## Mejoras pendientes

- **Historial persistente:** Actualmente el historial se guarda en base de datos, pero podría agregarse paginación y filtros avanzados.
- **Integración con más APIs:** Para obtener información más completa sobre los libros (reseñas, disponibilidad, etc.).
- **Seguridad:** Implementar autenticación y autorización para personalizar la experiencia y proteger datos.
- **Pruebas automatizadas:** Unit tests y tests de integración para garantizar calidad del código.



   












