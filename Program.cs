using Microsoft.AspNetCore.Authentication.Cookies;
using Quartz;
using WebApp;
using WebApp.Services.ExerciseServices;
using WebApp.Services.ImagesServices;
using WebApp.Services.MealPlanServices;
using WebApp.Services.MealServices;
using WebApp.Services.NotificationServices;
using WebApp.Services.TrainingServices;
using WebApp.Services.UsersServices;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5010);
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

});
builder.Services.AddDbContext<WebAppContext>();

builder.Services.AddHostedService<WeeklyNotificationService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITrainingService, TrainingService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IMealPlanService, MealPlanService>();
builder.Services.AddScoped<IImagesService, ImagesService>();
builder.Services.AddControllers();
builder.Logging.SetMinimumLevel(LogLevel.Information);
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

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
});

builder.Services.AddQuartzHostedService(opt =>
{
    opt.WaitForJobsToComplete = true;
});

builder.Services.AddHttpClient();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(); // wazne!
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSession();


app.Run();

