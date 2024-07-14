using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFramework;
using SocialPets.DBConnection;
using System.Configuration;
using Microsoft.OpenApi.Models;
using System.Text;



// Configura el DbContext con la cadena de conexión

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var serverVersion = new MySqlServerVersion(new Version(8,0));

builder.Services.AddDbContextPool<SocialPetsDB>(
   options => options.UseMySql("server=localhost;port=3306;database=socialpets;uid=root;password=password;", serverVersion)
);
//Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("./swagger.json", "My API V1");
    });



    app.MapSwagger();
}


// Configura la aplicación
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

















