namespace SoftUni.WebServer.Mvc.Interfaces
{
    public interface IViewable : IActionResult
    {
        IRenderable View { get; set; }
    }
}