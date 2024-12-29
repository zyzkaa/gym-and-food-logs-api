using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using WebApp;
using Microsoft.AspNetCore.Identity;
using WebApp.Entities;
using WebApp.Services.TrainingServices;
using WebApp.Services.UsersServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WebAppContext>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITrainingService, TrainingService>();
builder.Services.AddSingleton<ICurrentTrainingSerivce, CurrentTrainingService>();
builder.Services.AddControllers();
// builder.Services.AddAuthentication("cookie")
//     .AddCookie("cookie", options =>
//     {
//         options.LoginPath = "/user/login";
//     });
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
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

// builder.Services.AddDefault

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
// app.MapGet("/dayView", async (string day, WebAppContext dbContext) =>
// {
//     DateTime date = DateTime.Parse(day);
//     var trainings = dbContext.Trainings
//         .Where(training => training.Date == date);
//     return "day";
//
// }).WithName("GetDayView");
//
// app.MapPost("/register", (string username, string password, WebAppContext dbContext) =>
// {
//     dbContext.Users.Add(new User(username, password));
//     dbContext.SaveChanges();
//     return "success";
//     
// }).WithName("Register");
//
// app.MapGet("/login", async (string username, string password, WebAppContext dbContext, HttpContext httpContext) =>
// {
//     await httpContext.SignOutAsync("cookie");
//     var userFromDb = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
//     
//     if (userFromDb == null)
//     {
//         httpContext.Response.StatusCode = 401;
//         return null;
//     }
//
//     if (userFromDb.Password != password)
//     {
//         httpContext.Response.StatusCode = 401;
//         return null;
//     }
//     
//     var claims = new List<Claim>();
//     claims.Add(new Claim("id", userFromDb.Id.ToString()));
//     var identity = new ClaimsIdentity(claims, "cookie");
//     var user = new ClaimsPrincipal(identity);
//     
//     await httpContext.SignInAsync("cookie", user);
//     return "success";
// }).WithName("Login");
//
// app.UseAuthentication();
//
// app.MapGet("/usrId", (WebAppContext dbContext, HttpContext httpContext ) =>
// {
//     var user = httpContext.User.FindFirst("id").Value;
//     return user;
// });
//
// app.MapGet("/logout", (HttpContext httpContext) =>
// {
//     httpContext.SignOutAsync("cookie");
// });
//
// app.MapPost("/newTraining", (string name, WebAppContext dbContext, HttpContext httpContext) =>
// {
//     var userId = httpContext.User.FindFirst("id").Value;
//     var user = dbContext.Users.FirstOrDefault(u => u.Id.ToString() == userId);
//     var newTraining = new Training(name, DateTime.Now);
//     newTraining.User = user;
//     dbContext.Trainings.Add(newTraining);
//     dbContext.SaveChanges();
// });

app.Run();

