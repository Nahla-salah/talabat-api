using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParameters
    {

        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public  ProductSortingOptions sort { get; set; }
        public string? search { get; set; }

    }
}
