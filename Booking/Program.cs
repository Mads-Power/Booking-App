using BookingApp.Context;
using BookingApp.Helpers;
using BookingApp.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Reflection;

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
        options.GetClaimsFromUserInfoEndpoint = true;
    }, cookieOptions =>
    {
        cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        cookieOptions.Cookie.SameSite = SameSiteMode.Strict;
        cookieOptions.Cookie.HttpOnly = true;
    });

// Instead of adding [Authorize] to every endpoint
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddCors(options =>
{

    options.AddPolicy(name: "Client Origin",
                      builder => builder
                      .WithOrigins("https://app-prod-itv-officebooking.azurewebsites.net", "http://localhost:5002")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
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
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseCors("Client Origin");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html"); ;

app.Run();