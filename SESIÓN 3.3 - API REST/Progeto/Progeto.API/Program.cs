using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Progeto.API.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Agrega esta política CORS:
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ProgramDBContext>(options =>
    options.UseSqlite("Data Source=Programs.db"));


builder.Services.AddControllers(); // si usas controladores

var app = builder.Build();

// 🛠️ APLICA CORS AQUÍ (antes de MapControllers)
app.UseCors("AllowFrontend");

// Configuración del pipeline de la app
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // asegúrate de tener `[ApiController]` en tus controladores

app.Run();
