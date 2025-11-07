using Azure;
using E_Commers.Domain.Exceptions;
using E_commerse.Shared.ErrorModels;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace E_Commerse.Web.MiddleWars
{
    public class ExceptionsMiddlewares
    {
        private readonly RequestDelegate deleg;
        private readonly ILogger<ExceptionsMiddlewares> log;

        public ExceptionsMiddlewares(RequestDelegate deleg,ILogger<ExceptionsMiddlewares> log)
        {
            this.deleg = deleg;
            this.log = log;
        }
        public async Task invoke(HttpContext cont)
        {
            try
            {
                await deleg.Invoke(cont);
                if (cont.Response.StatusCode ==StatusCodes.Status404NotFound)
                {
                    throw new NotFoundException("not found page");
                }

            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                //Header
                cont.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                }; 

                    
                cont.Response.ContentType = "Application/json";
                //body
                var respons = new Errordata()
                {
                    statuscode = cont.Response.StatusCode,
                    massege=ex.Message
                };
                await cont.Response.WriteAsJsonAsync(respons);
                throw;
            }
        }
    }
            
    
}
