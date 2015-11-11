﻿using System.Web.Http;

namespace Shows.Example.Controllers
{
    [LogAction]
    public class HomeController : ApiController
    {
        public object Get()
        {
            return new { Title = "SHOWS", Description = "Self Hosted Opinionated Web Server", Status = ":)" };
        }
    }
}
