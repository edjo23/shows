using System;
using Shows.Example.Controllers;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var http = Shows.HttpServer.Create(s =>
            {
                s.Url = "http://localhost:2323";
                s.Container.Register<HomeController>();
            });

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
