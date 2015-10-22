using System;
using System.Net.Http;
using System.Web.Http.Filters;
using Common.Logging;
using Newtonsoft.Json;

namespace Shows
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            IoC.GetInstance<ILog>().Info(o => o("Incoming {0} {1} ({2}){3}{4}", actionContext.ActionDescriptor.ControllerDescriptor.ControllerName, actionContext.Request.Method.Method, actionContext.ActionDescriptor.ActionName, Environment.NewLine, JsonConvert.SerializeObject(actionContext.ActionArguments, Formatting.Indented)));

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                IoC.GetInstance<ILog>().Error(o => o("Exeception on {0} {1} ({2})", actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName, actionExecutedContext.ActionContext.Request.Method.Method, actionExecutedContext.ActionContext.ActionDescriptor.ActionName), actionExecutedContext.Exception);
            }
            else
            {
                IoC.GetInstance<ILog>().Info(o =>
                {
                    if (actionExecutedContext.Response.Content is ObjectContent)
                        o("Outgoing {0} {1} ({2}){3}{4}", actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName, actionExecutedContext.ActionContext.Request.Method.Method, actionExecutedContext.ActionContext.ActionDescriptor.ActionName, Environment.NewLine, JsonConvert.SerializeObject((actionExecutedContext.Response.Content as ObjectContent).Value, Formatting.Indented));
                    else
                        o("Outgoing {0} {1} ({2})", actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName, actionExecutedContext.ActionContext.Request.Method.Method, actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
                });
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
