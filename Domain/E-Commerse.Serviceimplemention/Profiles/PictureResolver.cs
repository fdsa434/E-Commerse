using AutoMapper;
using E_Commers.Domain.Models.Products;
using E_commerse.Shared.DTOS.ProductDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace E_Commerse.Serviceimplemention.Profiles
{
    public class PictureResolver(IConfiguration configuration) : IValueResolver<Product, GetProductDto, string>
    {
        public string Resolve(Product source, GetProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var url = $"{configuration.GetSection("Urls")["baseurl"]}{ source.PictureUrl}";
                return url;
            }
        }
    }
}
