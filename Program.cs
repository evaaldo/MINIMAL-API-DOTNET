using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINIMAL_API.Domains;
using MINIMAL_API.EndpointHandlers;
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

var rangosEndpoint = app.MapGroup("/rangos");
var rangosComIdEndpoint = rangosEndpoint.MapGroup("/{rangoId:int}");
var ingredientesEndpoints = rangosComIdEndpoint.MapGroup("/ingredientes");

rangosEndpoint.MapGet("", RangosHandlers.GetRangosAsync);

ingredientesEndpoints.MapGet("", async (
        RangoDbContext rangoDbContext,
        int rangoId,
        IMapper mapper
    ) => {
    return mapper.Map<IEnumerable<IngredienteDTO>>((await rangoDbContext.Rangos
                               .Include(rango => rango.Ingredientes)
                               .FirstOrDefaultAsync(rango => rango.Id == rangoId))?.Ingredientes);
});

rangosComIdEndpoint.MapGet("", async (
        RangoDbContext rangoDbContext,
        int rangoId,
        IMapper mapper
    ) => {
    return mapper.Map<RangoDTO>(await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId));
}).WithName("GetRangos");

rangosEndpoint.MapPost("", async Task<CreatedAtRoute<RangoDTO>>
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

    return TypedResults.CreatedAtRoute(
        rangoToReturn,
        "GetRangos",
        new { rangoId = rangoToReturn.Id }
    );

    // Referência para alunos
    // var linkToReturn = linkGenerator.GetUriByName(
    //     httpContext,
    //     "GetRango",
    //     new { id = rangoToReturn.Id }
    // );

    // return TypedResults.Created(linkToReturn, rangoToReturn);
});

rangosComIdEndpoint.MapPut("", async Task<Results<NotFound,Ok>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId,
        [FromBody] RangoParaAtualizacaoDTO rangoParaAtualizacaoDTO
    ) =>
    {
        var rangosEntity = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);
        if(rangosEntity == null)
            return TypedResults.NotFound();
        
        mapper.Map(rangoParaAtualizacaoDTO, rangosEntity);

        await rangoDbContext.SaveChangesAsync();

        return TypedResults.Ok();
    }
);

rangosComIdEndpoint.MapDelete("", async Task<Results<NotFound,NoContent>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId
    ) =>
    {
        var rangosEntity = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);
        if(rangosEntity == null)
            return TypedResults.NotFound();

        rangoDbContext.Rangos.Remove(rangosEntity);

        await rangoDbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
);

app.Run();
