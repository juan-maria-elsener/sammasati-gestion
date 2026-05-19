using Microsoft.EntityFrameworkCore;
using Sammasati.App.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("SammasatiConnection");
builder.Services.AddDbContext<SammasatiDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configuración de CORS: Permite que el frontend se conecte sin bloqueos
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()   // Permite que se conecte cualquier aplicación (ideal para la etapa de desarrollo)
              .AllowAnyHeader()   // Permite enviar cualquier tipo de dato (como JSON)
              .AllowAnyMethod();  // Permite ejecutar GET, POST, PUT, DELETE sin problemas
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Activamos la política de CORS
app.UseCors("PermitirTodo");
app.UseAuthorization();

app.MapControllers();

app.Run();