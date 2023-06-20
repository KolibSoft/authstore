using KolibSoft.Jwt.Server.Services;
using KolibSoft.Jwt.Server.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy("Allow-All", options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));
builder.Services.AddDbContext<DbContext>(options =>
{
    var connstring = "server=localhost;user=root;password=root;database=authstore;";
    options.UseMySql(connstring, ServerVersion.AutoDetect(connstring));
});
builder.Services.AddJwt();
builder.Services.AddAuthorization(options =>
{
    
});

var app = builder.Build();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

