using E_commerse.Shared.DTOS.ProductDtos;
using E_commerse.Shared.ProductQueryParam;
using E_commerse.Shared.Sorting;
using E_Commerse.ServiceAbstraction.IsurvaceManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace E_Commerse.Presentiton.Controllers.ProductController
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class ProductController(IserviceManager servicemanager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> getallproduct(ProductQueryParam param)
        {
            var res = await servicemanager.productservice.GetProductService( param);
            return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> getallproductbyid(int id)
        {
            var res = await servicemanager.productservice.GetProductByIdService(id);
            return Ok(res);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<GetBrandDto>>> getallbrands()
        {
            var res = await servicemanager.productservice.GetProductBrandService();
            return Ok(res);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<GetTypeDto>>> getalltypes()
        {
            var res = await servicemanager.productservice.GetProductTypeService();
            return Ok(res);
        }
    }
}
