using AutoMapper;
using E_Commers.Domain.Contracts.IUOW;
using E_Commers.Domain.Models.BrandModel;
using E_Commers.Domain.Models.Products;
using E_Commers.Domain.Models.Type_Model;
using E_commerse.Shared.DTOS.ProductDtos;
using E_Commerse.ServiceAbstraction.IService.IProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.Serviceimplemention.Serviceimplemetition.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GetBrandDto>> GetProductBrandService()
        {
           var productbrands= await unitOfWork.Gneraterepo<Brand, int>().getallrepo();
           return mapper.Map<IEnumerable<GetBrandDto>>(productbrands);
        }

        public async Task<GetProductDto> GetProductByIdService(int Id)
        {
            var products = await unitOfWork.Gneraterepo<Product, int>().getallbuidrepo(Id);
            return mapper.Map<GetProductDto>(products);
        }

        public async Task<IEnumerable<GetProductDto>> GetProductService()
        {
            var product = await unitOfWork.Gneraterepo<Product, int>().getallrepo();
            return mapper.Map<IEnumerable<GetProductDto>>(product);
        }

        public async Task<IEnumerable<GetTypeDto>> GetProductTypeService()
        {
            var producttypes = await unitOfWork.Gneraterepo<ProductType, int>().getallrepo();
            return mapper.Map<IEnumerable<GetTypeDto>>(producttypes);
        }

      
    }
}
