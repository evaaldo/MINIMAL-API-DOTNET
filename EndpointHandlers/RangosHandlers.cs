using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINIMAL_API.Domains;
using RangoAgil.API.DbContexts;
using RangoAgil.API.Entities;

namespace MINIMAL_API.EndpointHandlers;

public static class RangosHandlers
{
    public static async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>> GetRangoAsync
    (
        RangoDbContext rangoDbContext,
        ILogger<RangoDTO> logger,
        [FromQuery(Name = "name")] string? rangoNome,
        IMapper mapper
    )
    {
        var rangosEntity = await rangoDbContext.Rangos
            .Where(x => rangoNome == null || x.Name.ToLower().Contains(rangoNome.ToLower()))
            .ToListAsync();

        if(rangosEntity == null || rangosEntity.Count <= 0)
        {
            logger.LogInformation("Rango não encontrado");
            return TypedResults.NoContent();
        }
        else
        {
            logger.LogInformation("Retornando o Rango encontrado");
            return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));
        }
    }

    public static async Task<Ok<RangoDTO>> GetRangosComIdAsync
    (
        RangoDbContext rangoDbContext,
        int rangoId,
        IMapper mapper
    )
    {
        var rangosEntity = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);

        return TypedResults.Ok(mapper.Map<RangoDTO>(rangosEntity));
    }

    public static async Task<CreatedAtRoute<RangoDTO>> PostRangoAsync
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        [FromBody] RangoParaCriacaoDTO rangoParaCriacaoDTO
    )
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
    }

    public static async Task<Results<NotFound,Ok>> PutRangoComIdAsync
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId,
        [FromBody] RangoParaAtualizacaoDTO rangoParaAtualizacaoDTO
    )
    {
        var rangosEntity = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);

        if(rangosEntity == null)
            return TypedResults.NotFound();
        
        mapper.Map(rangoParaAtualizacaoDTO, rangosEntity);

        await rangoDbContext.SaveChangesAsync();

        return TypedResults.Ok();
    }

    public static async Task<Results<NotFound,NoContent>> DeleteRangoComIdAsync
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId
    )
    {
        var rangosEntity = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);

        if(rangosEntity == null)
            return TypedResults.NotFound();

        rangoDbContext.Rangos.Remove(rangosEntity);

        await rangoDbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}