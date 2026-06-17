var builder = WebApplication.CreateBuilder(args);

// Escuchar en el puerto 80 para toda la red local
builder.WebHost.UseUrls("http://*:80");

builder.Services.AddControllersWithViews();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Abierta", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Activar Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("Abierta");

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Productos}/{action=Index}/{id?}");

app.Run();