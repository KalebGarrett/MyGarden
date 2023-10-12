using Microsoft.EntityFrameworkCore;

using MyGarden.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString = Environment.GetEnvironmentVariable("AzureDbConnect");

builder.Services.AddDbContext<GardenDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDbConnect")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();

//Enable CORS
app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()
);

app.UseAuthorization();

app.MapControllers();

app.Run();