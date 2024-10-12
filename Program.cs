using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINIMAL_API.Domains;
using RangoAgil.API.DbContexts;
using RangoAgil.API.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("RangoDbConStr"))
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapGet("/", () => {
    return "Hello World";
});

app.MapGet("/rangos", async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>> (
        RangoDbContext rangoDbContext,
        [FromQuery(Name = "name")] string rangoNome,
        IMapper mapper
    ) => {
    var rangosEntity = await rangoDbContext.Rangos
        .Where(x => rangoNome == null || x.Name.ToLower().Contains(rangoNome.ToLower()))
        .ToListAsync();

    if(rangosEntity == null || rangosEntity.Count <= 0)
        return TypedResults.NoContent();
    else
        return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));
});

app.MapGet("/rango/{rangoId:int}/ingredientes", async (
        RangoDbContext rangoDbContext,
        int rangoId,
        IMapper mapper
    ) => {
    return mapper.Map<IEnumerable<IngredienteDTO>>((await rangoDbContext.Rangos
                               .Include(rango => rango.Ingredientes)
                               .FirstOrDefaultAsync(rango => rango.Id == rangoId))?.Ingredientes);
});

app.MapGet("/rango/{id:int}", async (
        RangoDbContext rangoDbContext,
        int id,
        IMapper mapper
    ) => {
    return mapper.Map<RangoDTO>(await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id));
});

app.Run();
