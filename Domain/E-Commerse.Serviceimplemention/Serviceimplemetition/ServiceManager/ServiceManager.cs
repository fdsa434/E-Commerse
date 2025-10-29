using AutoMapper;
using E_Commers.Domain.Contracts.IUOW;
using E_Commerse.ServiceAbstraction.IService.IProductService;
using E_Commerse.ServiceAbstraction.IsurvaceManager;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.Serviceimplemention.Serviceimplemetition.ServiceManager
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper mapper) : IserviceManager
    {
        public readonly Lazy<IProductService> _productService =new Lazy<IProductService>(() => new ProductService.ProductService(_unitOfWork, mapper));
        public IProductService productservice => _productService.Value;
    }
    
    
    
}
