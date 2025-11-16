using AutoMapper;
using E_Commers.Domain.Contracts.IUOW;
using E_Commers.Domain.Contracts.Reposatory.BasketRepo;
using E_Commers.Domain.Identity;
using E_Commerse.ServiceAbstraction.IService;
using E_Commerse.ServiceAbstraction.IService.IBasketService;
using E_Commerse.ServiceAbstraction.IService.IProductService;
using E_Commerse.ServiceAbstraction.IsurvaceManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.Serviceimplemention.Serviceimplemetition.ServiceManager
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper mapper, IBasketReposatory bas, UserManager<ApplicationUser> usermanager,IConfiguration con) : IserviceManager
    {
        public readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService.ProductService(_unitOfWork, mapper));
        public readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService.BasketService(bas, mapper));
        public readonly Lazy<IAuthorizationService> _AuthorizationService = new Lazy<IAuthorizationService>(() => new AuthorizationService.AuthorizationService(usermanager, con));


        public IBasketService basktservice => _basketService.Value;

        public IProductService productservice => _productService.Value;

        
      public IAuthorizationService AuthorizationService => _AuthorizationService.Value;

    }



}
