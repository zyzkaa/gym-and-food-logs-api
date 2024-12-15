using System.Runtime.InteropServices.JavaScript;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
            Link = linkGenerator.GetPathByName("GetDayView", new {day = dayByI})
        };
    }).ToArray();
    return days;
}).WithName("GetCalendar");

app.MapGet("/dayView", (DateTime day) =>
{
    return "day view " + day;
    
}).WithName("GetDayView");

app.Run();

