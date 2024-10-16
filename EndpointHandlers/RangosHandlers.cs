using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINIMAL_API.Domains;
using RangoAgil.API.DbContexts;

namespace MINIMAL_API.EndpointHandlers;

public static class RangosHandlers
{
    public static async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>> GetRangosAsync
    (
        RangoDbContext rangoDbContext,
        [FromQuery(Name = "name")] string? rangoNome,
        IMapper mapper
    )
    {
        var rangosEntity = await rangoDbContext.Rangos
            .Where(x => rangoNome == null || x.Name.ToLower().Contains(rangoNome.ToLower()))
            .ToListAsync();

        if(rangosEntity == null || rangosEntity.Count <= 0)
            return TypedResults.NoContent();
        else
            return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));
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
}