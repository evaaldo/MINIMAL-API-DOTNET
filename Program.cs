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

ingredientesEndpoints.MapGet("", IngredientesHandlers.GetIngredientesDoRango);

rangosComIdEndpoint.MapGet("", RangosHandlers.GetRangosComIdAsync).WithName("GetRangos");

rangosEndpoint.MapPost("", RangosHandlers.PostRangoAsync);

rangosComIdEndpoint.MapPut("", RangosHandlers.PutRangoComIdAsync);

rangosComIdEndpoint.MapDelete("", RangosHandlers.DeleteRangoComIdAsync);

app.Run();
