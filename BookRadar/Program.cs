using BookRadar.Data;
using BookRadar.Data.Repositories;
using BookRadar.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// 1. Agregar servicios al contenedor
// ----------------------------
builder.Services.AddControllersWithViews();

// Registro de servicios
builder.Services.AddHttpClient<IOpenLibraryService, OpenLibraryService>(); // Servicio API OpenLibrary
builder.Services.AddScoped<IBookRepository, BookRepository>(); // Repositorio de libros
builder.Services.AddScoped<IBookService, BookService>(); // Lógica de negocio de libros

// Configuración de EF Core con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ----------------------------
// 2. Configurar el pipeline HTTP
// ----------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Libros/Error"); // Cambiar a tu controlador
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Para CSS, JS, imágenes

app.UseRouting();
app.UseAuthorization();

// ----------------------------
// 3. Configurar rutas
// ----------------------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Libros}/{action=Index}/{id?}"); // Esto apunta a Libros

app.Run();