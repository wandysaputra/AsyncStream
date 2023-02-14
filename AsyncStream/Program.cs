using AsyncStream.DbContexts;
using AsyncStream.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// register the DbContext on the container 
builder.Services.AddDbContext<BooksContext>(options =>
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:BooksDBConnectionString"]));

builder.Services.AddScoped<IBooksRepository, BooksRepository>();

// register AutoMapper service and scan application assemblies that derive from Profile
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
