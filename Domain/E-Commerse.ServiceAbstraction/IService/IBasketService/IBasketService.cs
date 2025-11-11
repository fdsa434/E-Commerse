using E_commerse.Shared.DTOS.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.ServiceAbstraction.IService.IBasketService
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string key);
        Task<bool> deletBasketAsync(string key);
        Task<BasketDto?> CreateOrUpdateBasketAsync(BasketDto Basket, TimeSpan? timetolive = null);
    }
}
