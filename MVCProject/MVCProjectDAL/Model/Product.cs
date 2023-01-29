using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProjectDAL.Model
{
    public class Product : Base
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string? Model { get; set; }
        public string Img { get; set; } = null!;
        [NotMapped]
        public IFormFile file { get; set; }
        public decimal? UnitOfPrice { get; set; }
        public int TotalCount { get; set; }
        public string Description { get; set; }
        public int ProductCategoryId { get; set; }

        public ProductCategory? ProductCategory { get; set; } = null!;

    }
}
