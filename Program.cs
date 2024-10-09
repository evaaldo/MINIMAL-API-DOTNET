using Microsoft.EntityFrameworkCore;
using RangoAgil.API.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("RangoDbConStr"))
);

var app = builder.Build();

app.MapGet("/", () => {
    return "Hello World";
});

app.MapGet("/rango/{id}", (RangoDbContext rangoDbContext, int id) => {
    return rangoDbContext.Rangos.FirstOrDefault(x => x.Id == id);
});

app.MapGet("/rangos", (RangoDbContext rangoDbContext) => {
    return rangoDbContext.Rangos;
});

app.Run();
