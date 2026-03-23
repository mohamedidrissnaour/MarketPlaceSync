using System.Text.Json.Serialization;
using MasterCatalog.API;
using MasterCatalog.API.Middlewares;
using MasterCatalog.Application;
using MasterCatalog.Infrastructure;
using MasterCatalog.Infrastructure.Persistence.Data.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Infrastructure (MongoDB, Repositories)
builder.Services.AddInfrastructure(builder.Configuration);
// Config JWT
//builder.Services.AddJwtOptions(builder.Configuration);

// Application (Services métier)
builder.Services.AddApplication();


builder.Services.AddPresentation();



// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


var app = builder.Build();

// Seeding
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ProductSeeder>();
    await seeder.SeedAsync();
}

// --- Middleware global d'exceptions ---
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




app.Run();

