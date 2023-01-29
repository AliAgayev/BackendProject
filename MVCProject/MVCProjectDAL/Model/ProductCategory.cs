﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProjectDAL.Model
{
    public class ProductCategory : Base
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ICollection<Product> Products { get; set; }
    }
}
