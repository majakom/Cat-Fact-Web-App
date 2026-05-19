using CatFact.Application.Interfaces;
using CatFact.Application.Services;
using CatFact.Infrastructure.External;
using CatFact.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using CatFact.Infrastructure.Persistence;
using CatFact.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICatFactService, CatFactService>();

builder.Services.AddScoped<IFileStorageService, FileStorageService>();

builder.Services.AddScoped<IExportService, ExportService>();

builder.Services.AddHttpClient<ICatFactProvider, CatFactApiClient>();

builder.Services.AddDbContext<CatFactDbContext>(options =>
    options.UseSqlite("Data Source=catfacts.db"));

builder.Services.AddScoped<ICatFactRepository, CatFactRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CatFact}/{action=Index}/{id?}");

app.Run();