using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using task_slayer.Data.Entities;
using task_slayer.Data.Repositories;
using task_slayer.Data.Contexts;
using task_slayer.Data.Repositories.Implementations;
using task_slayer.Data.Repositories.Interfaces;
using task_slayer.Services.Implementations;
using task_slayer.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// 🔹 Configuração do banco de dados PostgreSQL
builder.Services.AddDbContext<TaskSlayerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TaskSlayerDB"))
);

// 🔹 Repositórios e Serviços
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();

builder.Services.AddScoped<ICategoriaService, CategoriaService>();

// 🔹 Configuração do Identity para usuários
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<TaskSlayerContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login"; // Caminho para a página de login
    options.AccessDeniedPath = "/login"; // Caminho para página de acesso negado
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
});

// 🔹 Configuração da autenticação via Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie();

// 🔹 Configuração de Autorização
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});


// 🔹 Habilitação do Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// 🔹 Configuração do Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseAuthentication(); // 🔹 Garante que a autenticação de Cookie sejam processadas
app.UseAuthorization();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();