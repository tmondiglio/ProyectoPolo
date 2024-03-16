using Datos.Contextos;
using Datos.Repositorios;
using Microsoft.EntityFrameworkCore;
using Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<HelloService>();
builder.Services.AddScoped<IImportServices, ImportServices>();
builder.Services.AddScoped<ISecurityServices, SecurityServices>();
builder.Services.AddScoped<ISecurityRepo, SecurityRepo>();

builder.Services.AddDbContext<SecurityContext>(opciones =>
{
  opciones.UseSqlServer(builder.Configuration.GetConnectionString("seguridad"));
  opciones.EnableDetailedErrors();
  opciones.EnableSensitiveDataLogging();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.Run();
