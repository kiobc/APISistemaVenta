using Microsoft.Extensions.Options;
using SistemaVentas.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InyectarDependencias(builder.Configuration);

builder.Services.AddCors(Options =>
{
    Options.AddPolicy("Nueva Politica", app =>
    {
        app.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Nueva Politica");

app.UseAuthorization();

app.MapControllers();

app.Run();
