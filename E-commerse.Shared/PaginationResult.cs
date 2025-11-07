using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerse.Shared
{
    public class PaginationResult<T>
    {
        public PaginationResult(int pageindex, int pagesize, int totalCoount, IEnumerable<T> data)
        {
            this.pageindex = pageindex;
            Pagesize = pagesize;
            TotalCoount = totalCoount;
            this.data = data;
        }

        public int pageindex { get; set; }
        public int Pagesize { get; set; }
        public int TotalCoount { get; set; }
        public IEnumerable<T> data { get; set; }



    }
}
