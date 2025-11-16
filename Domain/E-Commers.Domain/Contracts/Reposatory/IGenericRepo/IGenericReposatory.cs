using E_Commers.Domain.Contracts.ISpecification;
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
        public Task<IEnumerable<tentity>> getallspecificationrepo(ISpecification<tentity, tkey> spec);
       // public Task<int> getcountspecificationrepo(ISpecification<tentity, tkey> spec);

        public Task<tentity> getallspecificationByidrepo(ISpecification<tentity, tkey> spec);


        public Task<tentity> getallbuidrepo(tkey Id);
        public void AddEntity(tentity item);
        public void UpdateEntity(tentity item);
        public void DeleteAddEntity(tentity item);


    }
}
