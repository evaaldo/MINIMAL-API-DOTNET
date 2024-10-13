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
}).WithName("GetRango");

app.MapPost("/rango", async Task<CreatedAtRoute<RangoDTO>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        [FromBody] RangoParaCriacaoDTO rangoParaCriacaoDTO
        // LinkGenerator linkGenerator,
        // HttpContext httpContext
    ) =>
{
    var rangoEntity = mapper.Map<Rango>(rangoParaCriacaoDTO);
    rangoDbContext.Add(rangoEntity);
    await rangoDbContext.SaveChangesAsync();

    var rangoToReturn = mapper.Map<RangoDTO>(rangoEntity);

    return TypedResults.CreatedAtRoute(rangoToReturn, "GetRango", new { id = rangoToReturn.Id });

    // Referência para alunos
    // var linkToReturn = linkGenerator.GetUriByName(
    //     httpContext,
    //     "GetRango",
    //     new { id = rangoToReturn.Id }
    // );

    // return TypedResults.Created(linkToReturn, rangoToReturn);
});

app.MapPut("/rango/{id:int}", async Task<Results<NotFound,Ok>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int id,
        [FromBody] RangoParaAtualizacaoDTO rangoParaAtualizacaoDTO
    ) =>
    {
        var rangosEntity = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id);
        if(rangosEntity == null)
            return TypedResults.NotFound();
        
        mapper.Map(rangoParaAtualizacaoDTO, rangosEntity);

        await rangoDbContext.SaveChangesAsync();

        return TypedResults.Ok();
    }
);

app.MapDelete("/rango/{id:int}", async Task<Results<NotFound,NoContent>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int id
    ) =>
    {
        var rangosEntity = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id);
        if(rangosEntity == null)
            return TypedResults.NotFound();

        rangoDbContext.Rangos.Remove(rangosEntity);

        await rangoDbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
);

app.Run();
