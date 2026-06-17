using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURACIÓN DEL SERVIDOR (KESTREL)
// Forzamos a Kestrel a escuchar en todas las interfaces de red (0.0.0.0) 
// a través del puerto 8080 para evitar conflictos con el puerto 80 nativo de Windows.
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

// 2. AGREGAR SERVICIOS AL CONTENEDOR (DI)
builder.Services.AddControllersWithViews();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Política de CORS Abierta para permitir consumo de otras PCs, móviles o Frontends externos
builder.Services.AddCors(options =>
    options.AddPolicy("Abierta", p =>
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// 3. CONFIGURACIÓN DEL PIPELINE DE PETICIONES HTTP (MIDDLEWARES)

// Activar Swagger en la Raíz del sitio
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi Tienda API V1");
    c.RoutePrefix = ""; // Al ingresar a la IP limpia, cargará directamente Swagger
});

app.UseCors("Abierta");
app.UseStaticFiles();
app.UseRouting();

// Rutas para los Controladores de Vistas convencionales de MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Productos}/{action=Index}/{id?}");

// 4. ARRANCAR LA APLICACIÓN
app.Run();