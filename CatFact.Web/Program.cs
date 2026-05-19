using CatFact.Application.Interfaces;
using CatFact.Application.Services;
using CatFact.Infrastructure.External;
using CatFact.Infrastructure.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICatFactService, CatFactService>();

builder.Services.AddScoped<IFileStorageService, FileStorageService>();

builder.Services.AddHttpClient<ICatFactProvider, CatFactApiClient>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CatFact}/{action=Index}/{id?}");

app.Run();