using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerse.Shared.DTOS.BasketDtos
{
    public class BasketDto
    {
        public string Id { get; set; }
        public ICollection<BasketitemDto> items { get; set; }
    }
}
