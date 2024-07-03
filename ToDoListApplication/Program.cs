using DoList.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ToDoListApplication.Data;
using AppDbContext = ToDoListApplication.Data.AppDbContext;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Connection");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DoList.Data.Context.AppDbContext>(context =>
{
    context.UseNpgsql( connectionString);
});
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
 .AddJwtBearer(options =>
 {
     options.RequireHttpsMetadata = false;
     options.SaveToken = true;
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtSettings["ToDoListApplication"],
         ValidAudience = jwtSettings["ToDoListApplication"],
         IssuerSigningKey = new SymmetricSecurityKey(key)
     };
 });

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();    
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
