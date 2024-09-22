﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Parameter
{
    public class Department : BaseEntity
    {

        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

    }
}
