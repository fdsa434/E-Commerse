﻿using E_Commerse.ServiceAbstraction.IService.IProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.ServiceAbstraction.IsurvaceManager
{
    public interface IserviceManager
    {
        public IProductService productservice { get; }
    }
}
