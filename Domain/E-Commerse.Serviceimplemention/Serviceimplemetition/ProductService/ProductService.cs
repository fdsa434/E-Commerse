using AutoMapper;
using E_Commers.Domain.Contracts.IUOW;
using E_Commers.Domain.Models.BrandModel;
using E_Commers.Domain.Models.Products;
using E_Commers.Domain.Models.Type_Model;
using E_commerse.Shared.DTOS.ProductDtos;
using E_commerse.Shared.ProductQueryParam;
using E_commerse.Shared.Sorting;
using E_Commerse.ServiceAbstraction.IService.IProductService;
using Ecpmmerce.Persistance.Specification;
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

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GetBrandDto>> GetProductBrandService()
        {
            var productbrands = await unitOfWork.Gneraterepo<Brand, int>().getallrepo();
            return mapper.Map<IEnumerable<GetBrandDto>>(productbrands);
        }

        public async Task<GetProductDto> GetProductByIdService(int Id)
        {
            var specification = new ProductSpecification(Id);
            var products = await unitOfWork.Gneraterepo<Product, int>().getallspecificationByidrepo(specification);
            return mapper.Map<GetProductDto>(products);
        }

        public async Task<IEnumerable<GetProductDto>> GetProductService(ProductQueryParam param)
        {
            var specification = new ProductSpecification(param);
            var product = await unitOfWork.Gneraterepo<Product, int>().getallspecificationrepo(specification);
            return mapper.Map<IEnumerable<GetProductDto>>(product);
        }

        public async Task<IEnumerable<GetTypeDto>> GetProductTypeService()
        {
            var producttypes = await unitOfWork.Gneraterepo<ProductType, int>().getallrepo();
            return mapper.Map<IEnumerable<GetTypeDto>>(producttypes);
        }

    }
}
