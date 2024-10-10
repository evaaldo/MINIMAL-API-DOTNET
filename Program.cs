using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RangoAgil.API.DbContexts;
using RangoAgil.API.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("RangoDbConStr"))
);

var app = builder.Build();

app.MapGet("/", () => {
    return "Hello World";
});

app.MapGet("/rangos", async Task<Results<NoContent, Ok<List<Rango>>>> (RangoDbContext rangoDbContext, [FromQuery(Name = "name")] string rangoNome) => {
    var rangosEntity = await rangoDbContext.Rangos
        .Where(x => rangoNome == null || x.Name.ToLower().Contains(rangoNome.ToLower()))
        .ToListAsync();

    if(rangosEntity == null || rangosEntity.Count <= 0)
        return TypedResults.NoContent();
    else
        return TypedResults.Ok(rangosEntity);
});

app.MapGet("/rango/{id}", async (RangoDbContext rangoDbContext, int id) => {
    return await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id);
});

app.Run();
