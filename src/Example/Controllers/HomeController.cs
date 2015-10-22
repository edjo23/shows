using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;

namespace Shows.Example.Controllers
{
    [LogAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ApiController
    {
        public const string BaseWebPage = @"<!DOCTYPE html>
<html lang=""en"">
  <head>
    <meta charset=""utf-8"">
    <title>Maxgaming Loyalty</title>
  </head>
  <body>
    {0}
  </body>
</html>";

        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(String.Format(HomeController.BaseWebPage, ":)"));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
