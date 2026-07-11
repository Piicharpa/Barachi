using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Barachi.Models;

namespace Barachi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Delete(Barachi model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Save/process the delete record here
        _deleteService.ProcessDelete(model);

        // Branch based on type — this is the core routing logic
        if (model.Type == "15mg")
        {
            return RedirectToAction("Killdown", new { rbId = model.BarachiID });
        }
        else
        {
            return RedirectToAction("Retaping", new { rbId = model.BarachiID });
        }
    }

    public IActionResult Killdown(string rbId)
    {
        var chipLocations = _lotService.GetChipLocations(rbId);
        return View(chipLocations);
    }

    public IActionResult Retaping(string rbId)
    {
        var retapingInfo = _lotService.GetRetapingInfo(rbId);
        return View(retapingInfo);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
