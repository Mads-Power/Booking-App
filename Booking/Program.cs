using Microsoft.EntityFrameworkCore;
using Booking.Context;
using Booking.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Routing;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(UserService));
builder.Services.AddScoped(typeof(OfficeService));

builder.Services.AddDbContext<OfficeDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DevConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Client Origin",
                      builder => builder
                      .WithOrigins(Environment.GetEnvironmentVariable("FRONTEND_URL"))
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      );
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
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

////app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.UseCors("Client Origin");
app.UseAuthorization();



app.MapFallbackToFile("index.html"); ;

app.Run();
