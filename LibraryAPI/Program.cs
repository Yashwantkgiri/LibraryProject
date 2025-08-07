using LibraryAPI.Data;
using LibraryAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("LibraryDB");
builder.Services.AddDbContext<LibraryDBContext>(options =>
    options.UseSqlServer(connectionString));

    builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Headers.Append("Content-Type", "text/html");
        await context.Response.WriteAsync(
            "<h1>Welcome to the Library API</h1>" +
            "<p>Visit <a href='/swagger'>swagger</a> to explore the API.</p>"
        );
    }
    else
    {
        await next();
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
