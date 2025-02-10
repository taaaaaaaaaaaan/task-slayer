using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using task_slayer.Data.Entities;
using task_slayer.Data.Repositories;
using task_slayer.Data.Contexts;
using task_slayer.Data.Repositories.Implementations;
using task_slayer.Data.Repositories.Interfaces;
using task_slayer.Services.Implementations;
using task_slayer.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<TaskSlayerContext>()
               .AddDefaultTokenProviders();

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value);

builder.Services.AddDbContext<TaskSlayerContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("TaskSlayerDB"))
                );

IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<Usuario>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 3;
});
identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), builder.Services);
identityBuilder.AddEntityFrameworkStores<TaskSlayerContext>();
identityBuilder.AddRoleValidator<RoleValidator<IdentityRole>>();
identityBuilder.AddRoleManager<RoleManager<IdentityRole>>();
identityBuilder.AddSignInManager<SignInManager<Usuario>>();

  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/login";
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.Redirect("/login"); // Redireciona corretamente para a página de login
        return Task.CompletedTask;
    };
})

.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToFolder("/assets");
    options.Conventions.AllowAnonymousToFolder("/css");
    options.Conventions.AllowAnonymousToFolder("/lib");
    options.Conventions.AllowAnonymousToFolder("/js");
});
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

 
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages()
   .WithStaticAssets();

app.Run();
