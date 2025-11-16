using AutoMapper;
using E_Commers.Domain.Models.BasketModel;
using E_commerse.Shared.DTOS.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.Serviceimplemention.Profiles
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
            // BasketDto -> BasketCustomer
            CreateMap<BasketDto, BasketCustomer>()
                .ForMember(dest => dest.items, opt => opt.MapFrom(src => src.items));

            // BasketitemDto <-> BasketItems
            CreateMap<BasketitemDto, BasketItems>().ReverseMap();

            // BasketCustomer -> BasketDto
            CreateMap<BasketCustomer, BasketDto>()
                .ForMember(dest => dest.items, opt => opt.MapFrom(src => src.items));
        }
    }
}
