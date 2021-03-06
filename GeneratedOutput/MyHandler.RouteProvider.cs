//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Samples
{
    internal static class MyHandlerRouteExtensions
    {
        public static void MapHttpHandler<THttpHandler>(this Microsoft.AspNetCore.Routing.IEndpointRouteBuilder routes) where THttpHandler : Samples.MyHandler
        {
            var handler = new MyHandlerRoutes();
            routes.Map("/", handler.Get).WithMetadata(new uController.HttpGetAttribute());
            routes.Map("/blah", handler.Blah).WithMetadata(new uController.HttpGetAttribute());
            routes.Map("/status/{status}", handler.StatusCode).WithMetadata(new uController.HttpGetAttribute());
            routes.Map("/slow/status/{status}", handler.SlowTaskStatusCode).WithMetadata(new uController.HttpGetAttribute());
            routes.Map("/fast/status/{status}", handler.FastValueTaskStatusCode).WithMetadata(new uController.HttpGetAttribute());
            routes.Map("/lag", handler.DoAsync).WithMetadata(new uController.HttpGetAttribute());
            routes.Map("/hey/david", handler.HelloDavid).WithMetadata(new uController.HttpGetAttribute());
            routes.Map("/hey/{name?}", handler.GetAsync).WithMetadata(new uController.HttpGetAttribute());
            routes.Map("/hello", handler.Hello).WithMetadata(new uController.HttpGetAttribute());
            routes.Map("/", handler.Post).WithMetadata(new uController.HttpPostAttribute());
            routes.Map("/post-form", handler.PostAForm).WithMetadata(new uController.HttpPostAttribute());
            routes.Map("/auth", handler.Authed).WithMetadata(new uController.HttpGetAttribute(), new Microsoft.AspNetCore.Authorization.AuthorizeAttribute());
        }
        private class MyHandlerRoutes
        {
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public System.Threading.Tasks.Task Get(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var arg_context = httpContext;
                return Samples.MyHandler.Get(arg_context);
            }
            
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public System.Threading.Tasks.Task Blah(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var handler = new Samples.MyHandler();
                var result = handler.Blah();
                return httpContext.Response.WriteAsJsonAsync(result);
            }
            
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public System.Threading.Tasks.Task StatusCode(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var handler = new Samples.MyHandler();
                var arg_status_Value = httpContext.Request.RouteValues["status"]?.ToString();
                int arg_status;
                if (arg_status_Value == null || !int.TryParse(arg_status_Value, out arg_status))
                {
                    arg_status = default;
                }
                var result = handler.StatusCode(arg_status);
                return result.ExecuteAsync(httpContext);
            }
            
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public async System.Threading.Tasks.Task SlowTaskStatusCode(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var handler = new Samples.MyHandler();
                var result = await handler.SlowTaskStatusCode();
                await result.ExecuteAsync(httpContext);
            }
            
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public async System.Threading.Tasks.Task FastValueTaskStatusCode(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var handler = new Samples.MyHandler();
                var arg_loggerFactory = httpContext.RequestServices.GetRequiredService<Microsoft.Extensions.Logging.ILoggerFactory>();
                var result = await handler.FastValueTaskStatusCode(arg_loggerFactory);
                await result.ExecuteAsync(httpContext);
            }
            
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public System.Threading.Tasks.Task DoAsync(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var handler = new Samples.MyHandler();
                var arg_context = httpContext;
                var arg_q = httpContext.Request.Query["q"].ToString();
                return handler.DoAsync(arg_context, arg_q);
            }
            
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public System.Threading.Tasks.Task HelloDavid(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var handler = new Samples.MyHandler();
                var result = handler.HelloDavid();
                return httpContext.Response.WriteAsync(result);
            }
            
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public async System.Threading.Tasks.Task GetAsync(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var handler = new Samples.MyHandler();
                var arg_name = httpContext.Request.RouteValues["name"]?.ToString();
                var result = await handler.GetAsync(arg_name);
                await httpContext.Response.WriteAsync(result);
            }
            
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public async System.Threading.Tasks.Task Hello(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var handler = new Samples.MyHandler();
                var formCollection = await httpContext.Request.ReadFormAsync();
                var arg_s = formCollection["foo"].ToString();
                var arg_id = httpContext.Request.Headers["X-Id"].ToString();
                var arg_page_Value = httpContext.Request.Query["page"].ToString();
                System.Nullable<int> arg_page;
                if (arg_page_Value != null && int.TryParse(arg_page_Value, out var arg_page_Temp))
                {
                    arg_page = arg_page_Temp;
                }
                else
                {
                    arg_page = default;
                }
                var arg_pageSize_Value = httpContext.Request.Query["pageSize"].ToString();
                System.Nullable<int> arg_pageSize;
                if (arg_pageSize_Value != null && int.TryParse(arg_pageSize_Value, out var arg_pageSize_Temp))
                {
                    arg_pageSize = arg_pageSize_Temp;
                }
                else
                {
                    arg_pageSize = default;
                }
                var result = handler.Hello(arg_s, arg_id, arg_page, arg_pageSize);
                await httpContext.Response.WriteAsync(result);
            }
            
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public async System.Threading.Tasks.Task Post(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var handler = new Samples.MyHandler();
                var arg_obj = await httpContext.Request.ReadFromJsonAsync<Samples.Person>();
                var result = handler.Post(arg_obj);
                await result.ExecuteAsync(httpContext);
            }
            
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public async System.Threading.Tasks.Task PostAForm(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var handler = new Samples.MyHandler();
                var arg_form = await httpContext.Request.ReadFormAsync();
                handler.PostAForm(arg_form);
            }
            
            [System.Diagnostics.DebuggerStepThroughAttribute]
            public System.Threading.Tasks.Task Authed(Microsoft.AspNetCore.Http.HttpContext httpContext)
            {
                var handler = new Samples.MyHandler();
                handler.Authed();
                return System.Threading.Tasks.Task.CompletedTask;
            }
            
        }
    }
}
