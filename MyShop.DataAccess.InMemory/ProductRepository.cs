using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
   public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["products"]= products; //explicitly storing current product to cache
        }
        public void Insert(Product p)
        {
            products.Add(p); //Adding product to the "products" list
        }

        public void Update(Product product)
        {
            Product ProductToUpdate = products.Find(p=>p.Id==product.Id);
            if(ProductToUpdate!=null)
            {
                ProductToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found!!");
            }
        }

        public Product Find(string Id)
        {
            Product FindProduct= products.Find(p => p.Id == Id);
            if (FindProduct != null)
            {
                return FindProduct;
            }
            else
            {
                throw new Exception("Product not found!!");
            }
        }

        public IQueryable<Product> Collection() ///returns the list of products
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product DeleteProduct = products.Find(p => p.Id == Id);
            if (DeleteProduct != null)
            {
                products.Remove(DeleteProduct);
            }
            else
            {
                throw new Exception("Product not found!!");
            }
        }
    }
}
