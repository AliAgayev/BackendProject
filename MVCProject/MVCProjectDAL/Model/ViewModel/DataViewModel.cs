using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProjectDAL.Model.ViewModel
{
    public class DataViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
        public IEnumerable<Store> Stores { get; set; }
        public IEnumerable<Card> Cards { get; set; }
        
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderInf> OrderInfos { get; set; }
        
        
    }
}
