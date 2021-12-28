using UncleApp.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UncleApp.Services.Workers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UncleApp.ActionFilter;
using UncleApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;

using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

string ok = builder.Configuration.GetValue<string>("Jwt:Key");
byte[] b = Encoding.ASCII.GetBytes(ok);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DataContext")));
builder.Services.AddTransient<NotAuthenticatedOnly>();


builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DataContext>().AddSignInManager<SignInManager<IdentityUser>>().AddUserManager<UserManager<IdentityUser>>().AddRoleManager<RoleManager<IdentityRole>>();

//builder.Services.AddScoped<IJsonHandler, JsonTokenGenerator>();

builder.Services.AddAuthentication(options=>{
    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddCookie().AddJwtBearer(options =>
{
    
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(b),
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
    };

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// StartUpData.IntialData(app.Services);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );
});


app.Run();
