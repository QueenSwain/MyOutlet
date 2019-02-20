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
        List<Product> Products;

        public ProductRepository()
        {
            Products = cache["Products"] as List<Product>;
            if(Products==null)
            {
                Products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["Products"]=Products;
        }
        public void Insert(Product p)
        {
            Products.Add(p);
        }

        public void Update(Product product)
        {
            Product ProductToUpdate = Products.Find(p=>p.Id==product.Id);
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
            Product FindProduct= Products.Find(p => p.Id == Id);

            if (FindProduct != null)
            {
                return FindProduct;
            }
            else
            {
                throw new Exception("Product not found!!");
            }
        }

        // IQueryable is an interface that defines a data source that you can execute queries
        public IQueryable<Product> Collection()
        {
            return Products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product DeleteProduct = Products.Find(p => p.Id == Id);
            if (DeleteProduct != null)
            {
                Products.Remove(DeleteProduct);
            }
            else
            {
                throw new Exception("Product not found!!");
            }
        }
    }
}
