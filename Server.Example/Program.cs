using System.Text;
using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy("Allow-All", options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));
builder.Services.AddDbContext<AuthStoreContext>(options =>
{
    var connstring = "server=localhost;user=root;password=root;database=authstore;";
    options.UseMySql(connstring, ServerVersion.AutoDetect(connstring));
});
builder.Services.AddAuthorization(options =>
{

});
builder.Services.AddSingleton(new TokenGenerator(Encoding.UTF8.GetBytes("SECRET".GetHashString())));

var app = builder.Build();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

