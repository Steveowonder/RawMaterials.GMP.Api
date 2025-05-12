using RawMaterials.GMP.Api.Controllers;

WebApplicationBuilder builder = WebApplication
    .CreateBuilder(args)
    .Configure();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

IConfiguration configuration = builder.Configuration;

builder.Services.AddApiServices(builder.Configuration);

WebApplication app = builder.Build()
    .UseApi();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
