using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Service.DTOs.ProductCommand
{
    public class AddProductSpecificationDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
