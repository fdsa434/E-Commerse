using E_commerse.Shared.DTOS.BasketDtos;
using E_Commerse.ServiceAbstraction.IsurvaceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.Presentiton.Controllers.BasketController
{
    [ApiController]
    [Route("Api/[Controller]")]
    [Authorize]
        public class BasketController(IserviceManager service):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> getbasket(string key)
        {
            var res = await service.basktservice.GetBasketAsync(key);
            return  Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateorUpdatebasket(BasketDto basket)
        {
            var res = await service.basktservice.CreateOrUpdateBasketAsync(basket);
            return Ok(res);
        }
        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> Deletebasket(string key)
        {
            var res = await service.basktservice.deletBasketAsync(key);
            return Ok(res);
        }
    }
}
