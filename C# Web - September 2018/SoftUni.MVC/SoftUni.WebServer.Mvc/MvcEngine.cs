namespace SoftUni.WebServer.Mvc
{
    using Common;
    using Server;
    using System;
    using System.Reflection;

    public static class MvcEngine
    {
        public static void Run(HttpServer server)
        {
            ConfigureMvcContext(MvcContext.Get);

            while (true)
            {
                try
                {
                    server.Run();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void ConfigureMvcContext(MvcContext context)
        {
            context.AssemblyName = Assembly.GetEntryAssembly().GetName().Name;
            context.ControllersFolder = Constants.ControllersFolder;
            context.ControllerSuffix = Constants.ControllerSuffix;
            context.ModelsFolder = Constants.ModelsFolder;
            context.ViewsFolder = Constants.ViewsFolder;
        }
    }
}