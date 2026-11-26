using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Demo.LifeTime.Models;
using Demo.LifeTime.Services;

namespace Demo.LifeTime.Controllers;

public class HomeController(ISingleton singleton, ITransient transient, IScoped scoped, LifeTimeService service ) : Controller
{
    public IActionResult Index()
    {
        ViewBag.Singleton = singleton.Guid.ToString();
        ViewBag.Scoped = scoped.Guid.ToString();
        ViewBag.Transient = transient.Guid.ToString();

        ViewBag.ServiceSingleton = service.Singleton.Guid.ToString();
        ViewBag.ServiceScoped = service.Scoped.Guid.ToString();
        ViewBag.ServiceTransient = service.Transient.Guid.ToString();


        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
