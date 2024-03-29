using DataLayer;
using ProductApi.Controllers;
using ProductApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// create ProductDataStore instance anywhere its injected
builder.Services.AddSingleton<IProductDataStore, ProductDataStore>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>(provider =>
{
    var connectionString = "Server=127.0.0.1;port=3001;database=product;user id=root;password=root"; // Replace with your actual connection string
    return new ProductRepository(connectionString);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:7122") // Replace XXXX with your ProductApp port
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
}); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
