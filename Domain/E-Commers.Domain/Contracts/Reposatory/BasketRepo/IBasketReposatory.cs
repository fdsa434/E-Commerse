using E_Commers.Domain.Models.BasketModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Contracts.Reposatory.BasketRepo
{
    public interface IBasketReposatory
    {
        Task<BasketCustomer?> GetBasketAsync(string key);
        Task<bool> deletBasketAsync(string key);
        Task<BasketCustomer?> CreateOrUpdateBasketAsync(BasketCustomer Basket,TimeSpan? timetolive=null);

    }
}
