using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lab6.Controllers.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TimeController : ControllerBase
{
    [HttpGet("ConvertDate")]
    public ActionResult<string> ConvertDate([FromQuery] string date)
    {
        DateTime inputDateTime;

        if (DateTime.TryParse(date, out inputDateTime))
        {
            var kyivTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            var convertedDate = TimeZoneInfo.ConvertTime(inputDateTime, kyivTimeZone);

            return Ok(convertedDate.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        else
        {
            return BadRequest("Invalid date format.");
        }
    }
}
