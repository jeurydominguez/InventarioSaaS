using InventarioSaaS.API.Exepciones;
using InventarioSaaS.Application.service;
using InventarioSaaS.Domain.Entidades;
using InventarioSaaS.Domain.IRepository;
using InventarioSaaS.Domain.IService;
using InventarioSaaS.Infrastructure.ApplicationDbContext;
using InventarioSaaS.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";


//Area de servicios

builder.Services.AddControllers().AddNewtonsoftJson();

//configuracion del db context
builder.Services.AddDbContext<AppDbcontext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnections")));
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<AppDbcontext>()
    .AddDefaultTokenProviders();

//inyeccion de dependecnias
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepositorio>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IVentaService, VentaService>();

//configuracion del JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    });

builder.Services.AddAuthorization(option => option.AddPolicy("admin", politica => politica.RequireClaim("rol", "admin")));

builder.Services.AddSwaggerGen();

var app = builder.Build();

// area de Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<MiddlewareEx>();

app.UseAuthentication();

app.UseAuthorization();

app.Urls.Add($"http://*:{port}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
