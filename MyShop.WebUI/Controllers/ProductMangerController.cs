using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductMangerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository ProductCategories;

        public ProductMangerController()
        {
            context = new ProductRepository();
            ProductCategories = new ProductCategoryRepository();

        }
        // GET: ProductManger
        public ActionResult Index()
        {
            List<Product> Products = context.Collection().ToList();
           
            return View(Products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.ProductCategories = ProductCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product Createproduct)
        {
            //ModelState.IsValid is the only way to know whether there were any validation(or data conversion) errors during model binding
            if (!ModelState.IsValid)
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
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = ProductCategories.Collection();

                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product,string Id)
        {
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

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