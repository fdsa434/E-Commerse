using AutoMapper;
using E_Commers.Domain.Identity;
using E_Commers.Domain.Models.BasketModel;
using E_Commers.Domain.Models.BrandModel;
using E_Commers.Domain.Models.Products;
using E_Commers.Domain.Models.Type_Model;
using E_commerse.Shared.DTOS.BasketDtos;
using E_commerse.Shared.DTOS.IdentityDtos;
using E_commerse.Shared.DTOS.ProductDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.Serviceimplemention.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile(IConfiguration con)
        {
            CreateMap<Product, GetProductDto>()
                .ForMember(des => des.BrandName, src => src.MapFrom(p => p.Productbrand.Name))
                .ForMember(des => des.TypeName, src => src.MapFrom(p => p.ProductType.Name))
                .ForMember(des => des.PictureUrl, src => src.MapFrom(new PictureResolver( con)))
                .ReverseMap();

            CreateMap<Brand, GetBrandDto>().ReverseMap();
            CreateMap<ProductType, GetTypeDto>().ReverseMap();
          



        }
    }
}
