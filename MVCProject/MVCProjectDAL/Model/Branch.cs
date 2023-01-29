using System;
using System.Collections.Generic;

namespace MVCProjectDAL.Model
{
    public class Branch : Base
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; } 
        public int StoreId { get; set; }

        public Store? Store { get; set; }
    }
}
