using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProjectDAL.Model
{
    public class OrderInf : Base
    {
        public int ProductCount { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
