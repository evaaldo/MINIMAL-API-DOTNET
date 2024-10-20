using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RangoAgil.API.DbContexts;
using Microsoft.AspNetCore.Http.HttpResults;
using MINIMAL_API.Domains; // Certifique-se de incluir isso

namespace MINIMAL_API.EndpointHandlers;

public class IngredientesHandlers
{
    public static async Task<Results<NoContent, Ok<IEnumerable<IngredienteDTO>>>> GetIngredientesAsync
    (
        RangoDbContext rangoDbContext,
        int rangoId,
        IMapper mapper
    ) 
    {
        var rangoEntity = await rangoDbContext.Rangos
                               .Include(rango => rango.Ingredientes)
                               .FirstOrDefaultAsync(rango => rango.Id == rangoId);

        if (rangoEntity?.Ingredientes == null || !rangoEntity.Ingredientes.Any())
        {
            return TypedResults.NoContent();
        }

        var ingredientesDto = mapper.Map<IEnumerable<IngredienteDTO>>(rangoEntity.Ingredientes);
        return TypedResults.Ok(ingredientesDto);
    }
}
