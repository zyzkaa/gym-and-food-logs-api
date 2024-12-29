using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;

namespace WebApp.Controllers;

[ApiController]
[Authorize]
[Route("calendar")]
public class CalendarController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CalendarController(IHttpContextAccessor httpContextAccessor)
    {   
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("set_today")]
    public string SetToday()
    {
        var today = DateTime.Today.ToString("yyyy-MM-dd");
        _httpContextAccessor.HttpContext.Session.SetString("date", today);
        return today;
    }

    [HttpGet("get_date")]
    public DateTime GetDate()
    {
        return DateTime.Parse(_httpContextAccessor.HttpContext.Session.GetString("date"));
    }

    [HttpPost("change_date")]
    public DateTime ChangeDate([FromBody] DateDto dateDto)
    {
        var newDate = new DateTime(dateDto.Year, dateDto.Month, dateDto.Day);
        _httpContextAccessor.HttpContext.Session.SetString("date", newDate.ToString("yyyy-MM-dd"));
        return newDate;
    }
}