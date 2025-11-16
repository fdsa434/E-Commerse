using AutoMapper;
using E_Commers.Domain.Contracts.Reposatory.BasketRepo;
using E_Commers.Domain.Exceptions;
using E_Commers.Domain.Models.BasketModel;
using E_commerse.Shared.DTOS.BasketDtos;
using E_Commerse.ServiceAbstraction.IService.IBasketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.Serviceimplemention.Serviceimplemetition.BasketService
{
    public class BasketService(IBasketReposatory repo,IMapper mapper) : IBasketService
    {
        public async Task<BasketDto?> CreateOrUpdateBasketAsync(BasketDto Basket, TimeSpan? timetolive = null)
        {
            var bas = mapper.Map<BasketDto, BasketCustomer>(Basket);
            var addbs = await repo.CreateOrUpdateBasketAsync( bas);
            if (addbs is not null)
            {
                return await GetBasketAsync(bas.Id);
            }
            else
            {
                throw new Exception("not added");
            }
        }

        public async Task<bool> deletBasketAsync(string key)
        {
            return await repo.deletBasketAsync(key);
        }

        public async Task<BasketDto?> GetBasketAsync(string key)
        {
            var basket = await repo.GetBasketAsync(key);
            if(basket is not null)
            {
               return  mapper.Map<BasketDto>(basket);
            }
            else
            {
                throw new BasketNotFoundEX(key);
            }
            
        }
    }
}
