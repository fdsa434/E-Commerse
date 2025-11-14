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
                var respons = new Errordata()
                {
                    massege = ex.Message
                };
                //Header
                cont.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestEX badex => GetBadRequest(badex, respons),

                    _ => StatusCodes.Status500InternalServerError
                }; 

                    
                cont.Response.ContentType = "Application/json";
                //body
                respons.statuscode = cont.Response.StatusCode;
                await cont.Response.WriteAsJsonAsync(respons);
                throw;
            }
        }
        public int GetBadRequest(BadRequestEX ex,Errordata response)
        {
            response.errors = ex.errors;
            return StatusCodes.Status400BadRequest;
        }

    }


}
