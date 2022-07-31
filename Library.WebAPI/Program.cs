using Library.WebAPI.Infrastructure.Database;
using Library.WebAPI.Interfaces;
using Library.WebAPI.Mappings;
using Library.WebAPI.Middlewares;
using Library.WebAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("LibraryDatabase");

builder.Services.AddDbContext<LibraryDbContext>(options => options.UseMySql(connectionString, new MariaDbServerVersion("10.6")));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddAutoMapper(typeof(AutoMappings));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ExceptionMiddleware>();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
