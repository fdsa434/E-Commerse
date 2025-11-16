using E_Commers.Domain.Contracts.Reposatory.BasketRepo;
using E_Commers.Domain.Exceptions;
using E_Commers.Domain.Models.BasketModel;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Ecpmmerce.Persistance.Reposatory.GenericReposatoty
{
    public class BasketReposatory(IConnectionMultiplexer connect) : IBasketReposatory
    {
        private readonly IDatabase redis = connect.GetDatabase();
        public async Task<BasketCustomer?> CreateOrUpdateBasketAsync(BasketCustomer Basket, TimeSpan? timetolive = null)
        {
            var bas = JsonSerializer.Serialize(Basket);
            var iscreatedorupdated = redis.StringSet(Basket.Id, bas, timetolive);
            if (iscreatedorupdated)
            {
                return await GetBasketAsync(Basket.Id);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> deletBasketAsync(string key)
        {
            return await redis.KeyDeleteAsync(key);
        }

        public async Task<BasketCustomer?> GetBasketAsync(string key)
        {
            var basket = await redis.StringGetAsync(key);
            if (basket.IsNullOrEmpty)
                throw new BasketNotFoundEX(key);
            var basketjson = JsonSerializer.Deserialize<BasketCustomer>(basket);
            if(basketjson is null)
            {
                return null;
            }
            return basketjson;

        }
    }
}
