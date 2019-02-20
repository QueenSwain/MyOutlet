using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModel
{
  public class ProductManagerViewModel
    {
        public Product Product { get; set; }
        //ienumerable used for iterating the category.when ever will create product .the list of category will show in ddl.
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
