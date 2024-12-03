using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleDrive.Repositories;
using SimpleDrive.Services;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient();
//builder.Services.AddScoped<IStorageService, S3StorageService>;

builder.Services.AddScoped<IStorageService>(provider =>
{
    var storagePath = builder.Configuration.GetValue<string>("StoragePath");
    return new LocalStorageService(storagePath); 
}); builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "SimpleDrive",
            ValidAudience = "SimpleDriveClients",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12121212"))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
