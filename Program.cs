using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using WebApp;
using WebApp.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WebAppContext>();
builder.Services.AddAuthentication("cookie").AddCookie("cookie");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var month = DateTime.Today.Month;
app.MapGet("/calendar", (HttpContext context) =>
{
    var linkGenerator = context.RequestServices.GetRequiredService<LinkGenerator>();
    int daysNum = DateTime.DaysInMonth(DateTime.Now.Year, month);
    
    var days = Enumerable.Range(1, daysNum).Select(i =>
    {
        var dayByI = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
        return new
        {
            Day = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i),
            // Link = linkGenerator.GetPathByName("GetDayView", new {dayString = day.ToString("yyyy-MM-dd")})
            Link = linkGenerator.GetPathByName("GetDayView", new {day = dayByI.ToString("yyyy-MM-dd")})
        };
    }).ToArray();
    return days;
}).WithName("GetCalendar");

app.MapGet("/dayView", async (string day, WebAppContext dbContext) =>
{
    DateTime date = DateTime.Parse(day);
    var trainings = dbContext.Trainings
        .Where(training => training.Date == date);
    return "day";

}).WithName("GetDayView");

app.MapPost("/register", (string username, string password, WebAppContext dbContext) =>
{
    dbContext.Users.Add(new User(username, password));
    dbContext.SaveChanges();
    return "success";
    
}).WithName("Register");

app.MapGet("/login", async (WebAppContext dbContext, HttpContext httpContext) =>
{
    var claims = new List<Claim>();
    claims.Add(new Claim("id", "1"));
    var identity = new ClaimsIdentity(claims, "cookie");
    var user = new ClaimsPrincipal(identity);
    
    await httpContext.SignInAsync("cookie", user);
    return "success";
}).WithName("Login");

app.UseAuthentication();

app.MapGet("/usrId", (WebAppContext dbContext, HttpContext httpContext ) =>
{
    var user = httpContext.User.FindFirst("id").Value;
    return user;
});

app.MapGet("/logout", (HttpContext httpContext) =>
{
    httpContext.SignOutAsync("cookie");
});

app.Run();

