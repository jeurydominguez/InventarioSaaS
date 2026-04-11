using InventarioSaaS.Application.EX;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InventarioSaaS.API.Exepciones
{
    public class MiddlewareEx
    {
        private readonly RequestDelegate next;

        public MiddlewareEx(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await ManejarExepcion(context, ex);
            }
        }

        private async Task ManejarExepcion(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case NotFoundEx:
                    context.Response.StatusCode = 404;
                    break;

                case ArgumentEx:
                    context.Response.StatusCode = 400;
                    break;

                case NoContentEx:
                    context.Response.StatusCode = 204;
                    return;

                default:
                    context.Response.StatusCode = 500;
                    break;
            }

            var respuesta = new
            {
                mensaje = ex.Message
            };

            await context.Response.WriteAsJsonAsync(respuesta);
        }
    }
}
