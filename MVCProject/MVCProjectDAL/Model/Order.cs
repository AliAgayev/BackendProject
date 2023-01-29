using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProjectDAL.Model
{
    public class Order : Base
    {
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<OrderInf>? OrderInfos { get; set; }
    }
}
