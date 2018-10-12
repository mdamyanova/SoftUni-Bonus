namespace SoftUni.WebServer.Mvc.ActionResults
{
    using Interfaces;

    public class ViewResult : IViewable
    {
        public ViewResult(IRenderable view)
        {
            this.View = view;
        }

        public IRenderable View { get; set; }

        public string Invoke()
            => this.View.Render();
    }
}