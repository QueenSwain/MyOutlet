using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductMangerController : Controller
    {
        ProductRepository context;
        public ProductMangerController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManger
        public ActionResult Index()
        {
            List<Product> Products = context.Collection().ToList();
            return View(Products);
        }

        public ActionResult Create()
        {
            Product prod = new Product();
            return View(prod);
        }

        [HttpPost]
        public ActionResult Create(Product Createproduct)
        {
            //ModelState.IsValid is the only way to know whether there were any validation(or data conversion) errors during model binding
            if(!ModelState.IsValid)
            {
                return View(Createproduct);
            }
            else
            {
                context.Insert(Createproduct);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product ProdEdit = context.Find(Id);
            if(ProdEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProdEdit);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product,string Id)
        {
            Product ProductToEdit = context.Find(Id);
              if (ProductToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if(!ModelState.IsValid)
                {
                    return View(product);
                }
                ProductToEdit.Category = product.Category;
                ProductToEdit.Description = product.Description;
                ProductToEdit.Name = product.Name;
                ProductToEdit.Price = product.Price;
                ProductToEdit.Image = product.Image;

                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product ProdToDelete = context.Find(Id);
            if (ProdToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProdToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product ProductToDleete = context.Find(Id);
            if (ProductToDleete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}