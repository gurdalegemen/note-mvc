using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Notes.Data.Context;
using Notes.Data.Repository.Abstract;
using Notes.Data.Repository.Concrete;
using Notes.Data.Unitofwork.Abstract;
using Notes.Data.Unitofwork.Concrete;
using Notes.Service.Abstract;
using Notes.Service.Concrete;
using Notes.Service.Mapper;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseNpgsql(builder.Configuration.GetConnectionString("dbConnection"));
    });

builder.Services.AddScoped<INoteService, NoteService>();

builder.Services.AddScoped<INoteRepository, NoteRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Notes API",
        Description = "An ASP.NET Core Web API for taking Notes",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//    options.RoutePrefix = string.Empty;
//});

app.UseHttpMethodOverride(); // Enables method overriding based on the _method field

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
