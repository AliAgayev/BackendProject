using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProjectDAL.Model
{
    public  class Store : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Branch> Branches { get; set; }
    }
}
