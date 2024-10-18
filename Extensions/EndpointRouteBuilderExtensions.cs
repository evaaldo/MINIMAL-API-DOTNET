using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MINIMAL_API.EndpointHandlers;

namespace MINIMAL_API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterRangosEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var rangosEndpoint = endpointRouteBuilder.MapGroup("/rangos");
        var rangosComIdEndpoint = rangosEndpoint.MapGroup("/{rangoId:int}");

        rangosEndpoint.MapGet("", RangosHandlers.GetRangoAsync);

        rangosComIdEndpoint.MapGet("", RangosHandlers.GetRangosComIdAsync).WithName("GetRangos");

        rangosEndpoint.MapPost("", RangosHandlers.PostRangoAsync);

        rangosComIdEndpoint.MapPut("", RangosHandlers.PutRangoComIdAsync);

        rangosComIdEndpoint.MapDelete("", RangosHandlers.DeleteRangoComIdAsync);
    }

    public static void RegisterIngredientesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var ingredientesEndpoints = endpointRouteBuilder.MapGroup("/rangos/{rangoId:int}/ingredientes");
        ingredientesEndpoints.MapGet("", IngredientesHandlers.GetIngredientesAsync);
    }
}