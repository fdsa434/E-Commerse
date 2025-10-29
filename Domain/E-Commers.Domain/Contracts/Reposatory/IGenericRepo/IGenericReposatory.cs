using E_Commers.Domain.Models;
using E_Commers.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Contracts.Reposatory.IGenericRepo
{
    public interface IGenericReposatory<tentity,tkey> where tentity:BaseEntity<tkey>
    {
        public Task<IEnumerable<tentity>> getallrepo();
        public Task<tentity> getallbuidrepo(tkey Id);
        public void AddEntity(tentity item);
        public void UpdateEntity(tentity item);
        public void DeleteAddEntity(tentity item);


    }
}
