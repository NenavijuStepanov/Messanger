using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using messanger.Controllers;
using messanger.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Обязательно для Swagger
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")               // достает строку подключения
    ?? throw new InvalidOperationException("Строка подключения 'DefaultConnection' не найдена.");

builder.Services.AddDbContext<DB>(options =>                                             // штука, чтоб проверить работу бд 
    options.UseNpgsql(connectionString));


builder.Services.AddScoped<IPasswordHasher<UserDB>, PasswordHasher<UserDB>>();   // регистрация хешера паролей


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DB>();
    dbContext.Database.Migrate();
}

app.Run();
