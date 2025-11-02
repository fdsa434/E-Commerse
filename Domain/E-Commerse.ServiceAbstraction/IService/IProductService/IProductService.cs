using E_commerse.Shared.DTOS.ProductDtos;
using E_commerse.Shared.ProductQueryParam;
using E_commerse.Shared.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.ServiceAbstraction.IService.IProductService
{
    public interface IProductService
    {
        public Task<IEnumerable<GetProductDto>> GetProductService(ProductQueryParam param);
        public Task<GetProductDto> GetProductByIdService(int Id);

        public Task<IEnumerable<GetTypeDto>> GetProductTypeService();
        public Task<IEnumerable<GetBrandDto>> GetProductBrandService();


    }
}
