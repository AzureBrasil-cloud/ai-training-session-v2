using Microsoft.AspNetCore.Mvc;

namespace PowerPilotChat.Web.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    public IActionResult Index()
    {
        return Ok();
    }
}