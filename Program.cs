using System.Net;
using Microsoft.EntityFrameworkCore;
using MINIMAL_API.Extensions;
using RangoAgil.API.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("RangoDbConStr"))
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddProblemDetails();

var app = builder.Build();

if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.RegisterRangosEndpoints();
app.RegisterIngredientesEndpoints();

app.Run();
