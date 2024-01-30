using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace WebApplication8.Middleware
{
    public class SweetAlertMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);

            // Check if a new post was created
            if (context.Request.Path == "/Post/Create" && context.Request.Method == "POST")
            {
                // Delay the alert for 5 minutes
                await Task.Delay(TimeSpan.FromMinutes(1));

                // Trigger the SweetAlert
                // You can use a library like SweetAlert.js or any other alert library of your choice
                // This example uses a JavaScript alert as a placeholder
                await context.Response.WriteAsync("<script>alert('5 minutes have passed since creating the post!');</script>");
            }
        }

    }
}
