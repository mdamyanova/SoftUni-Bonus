namespace SoftUni.SimpleApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        public IActionResult Index()
            => this.View();

        public IActionResult About()
            => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            =>  this.View(new ErrorViewModel
            { RequestId = Activity.Current?.Id ??
                HttpContext.TraceIdentifier });
    }
}