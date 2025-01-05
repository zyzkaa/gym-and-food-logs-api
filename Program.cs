using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using WebApp;
using Microsoft.AspNetCore.Identity;
using WebApp.Entities;
using WebApp.Services.MealPlanServices;
using WebApp.Services.MealServices;
using WebApp.Services.TrainingServices;
using WebApp.Services.UsersServices;

var builder = WebApplication.CreateBuilder(args);

// trzeba zmienic nazwe repository !!!!!!!!!!!!!!!!!!!!!!!!!!1


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

});
builder.Services.AddDbContext<WebAppContext>();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITrainingService, TrainingService>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IMealPlanService, MealPlanService>();

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "cookie";
        options.LoginPath = "/user/login";
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.ApplyMigrations();
}   

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSession();


// leaving the endpoint from the template
// var month = DateTime.Today.Month;
// app.MapGet("/calendar", (HttpContext context) =>
// {
//     var linkGenerator = context.RequestServices.GetRequiredService<LinkGenerator>();
//     int daysNum = DateTime.DaysInMonth(DateTime.Now.Year, month);
//     
//     var days = Enumerable.Range(1, daysNum).Select(i =>
//     {
//         var dayByI = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
//         return new
//         {
//             Day = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i),
//             // Link = linkGenerator.GetPathByName("GetDayView", new {dayString = day.ToString("yyyy-MM-dd")})
//             Link = linkGenerator.GetPathByName("GetDayView", new {day = dayByI.ToString("yyyy-MM-dd")})
//         };
//     }).ToArray();
//     return days;
// }).WithName("GetCalendar");
//


app.Run();

