using Microsoft.EntityFrameworkCore;
using BookingApp.Context;
using BookingApp.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using System.Reflection;
using BookingApp.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// Authentication
builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(options =>
    {
        builder.Configuration.Bind("AzureAd", options);
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.ResponseType = OpenIdConnectResponseType.Code;


    }, cookieOptions =>
    {
        cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        cookieOptions.Cookie.SameSite = SameSiteMode.Strict; //SameSiteMode.Strict; May require frontend and backend to run on same port when running locally
        cookieOptions.Cookie.HttpOnly = true;
    });

builder.Services.AddCors(options =>
{

    options.AddPolicy(name: "Client Origin",
                      builder => builder
                      .AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      //.AllowCredentials()
                      //.WithOrigins("http://localhost:5173")
    );
});

builder.Services.AddAutoMapper(typeof(Program));

// Add dependency injection containers
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IOfficeRepository,OfficeRepository>();
builder.Services.AddScoped<IRoomRepository,RoomRepository>();
builder.Services.AddScoped<ISeatRepository,SeatRepository>();
builder.Services.AddScoped<IBookingRepository,BookingRepository>();
builder.Services.AddScoped<IDateTimeProvider,DateTimeProvider>();

builder.Services.AddDbContext<OfficeDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DevConnection")));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});



app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseCors("Client Origin");
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.MapFallbackToFile("index.html"); ;

app.Run();
