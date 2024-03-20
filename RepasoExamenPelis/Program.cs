using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RepasoExamenPelis.Data;
using RepasoExamenPelis.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//HABILITAMOS SESSION DENTRO DE NUESTRO SERVIDOR
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//HABILITAMOS LA SEGURIDAD EN SERVICIOS
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();
// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("SqlPelis");
builder.Services.AddTransient<RepositoryUsuarios>();
builder.Services.AddTransient<RepositoryPeliculas>();
builder.Services.AddDbContext<PeliculasContext>
    (options => options.UseSqlServer(connectionString));
//PERSONALIZAMOS NUESTRAS RUTAS
builder.Services.AddControllersWithViews
    (options => options.EnableEndpointRouting = false)
    .AddSessionStateTempDataProvider();
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

app.UseSession();
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
