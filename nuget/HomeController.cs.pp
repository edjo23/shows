using System.Web.Http;
using Shows;

namespace $rootnamespace$.Controllers
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
