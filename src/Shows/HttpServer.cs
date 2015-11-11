using System;
using System.IO;
using System.Web.Http;
using Common.Logging;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

[assembly: OwinStartup(typeof(Shows.HttpServerStartup))]
namespace Shows
{
    public class HttpServer
    {
        public static HttpServer Create(Action<HttpServer> setup = null)
        {
            var httpServer = new HttpServer();
            httpServer.Start(setup);
            return httpServer;
        }

        public Container Container { get; private set; }

        public IDisposable WebServer { get; private set; }

        public string Url { get; set; }

        protected virtual void Start(Action<HttpServer> setup = null)
        {
            Container = new Container();
            Container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            Container.RegisterSingleton<HttpServer>(() => this);
            Container.RegisterSingleton<ILog>(() => LogManager.GetLogger(""));

            IoC.Container = Container;

            if (setup != null)
                setup(this);

            if (Url == null)
                Url = "http://*:80";

            WebServer = WebApp.Start(Url);
        }

        protected void Stop()
        {
            WebServer.Dispose();
            WebServer = null;
        }
    }

    public class HttpServerStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure WebAPI.
            var config = new HttpConfiguration { DependencyResolver = new SimpleInjectorWebApiDependencyResolver(IoC.Container) };

            // ApiControllers.
            config.Routes.MapHttpRoute("API Default", "api/{Controller}");
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();

            app.UseWebApi(config);

            // File Server.
            Directory.CreateDirectory(@".\StaticRoot");

            var packageFileServiceOptions = new FileServerOptions()
            {
                RequestPath = new PathString(""),
                FileSystem = new PhysicalFileSystem(@".\StaticRoot")
            };
            packageFileServiceOptions.StaticFileOptions.ServeUnknownFileTypes = true;
            packageFileServiceOptions.DefaultFilesOptions.DefaultFileNames.Add("home.html");

            app.UseFileServer(packageFileServiceOptions);            
        }
    }
}
