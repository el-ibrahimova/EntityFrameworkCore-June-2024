using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProductShop.DTOs.Export
{
    public class ExportProductDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Seller { get; set; }
    }
}
