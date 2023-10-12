using Olx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Olx.Controllers
{
    public class AdminController : Controller
    {
        DataAccess dataAccess = null;
        public AdminController()
        {
            dataAccess = new DataAccess();
        }

        public ActionResult ProductList(string SearchItem, int? i)
        {
            IEnumerable<ProductListModel> products = dataAccess.GetAllProductList();
            return View(products);
        }

        public ActionResult ProductListDetails(int advertiseId)
        {
            ProductListModel product = dataAccess.GetProductList(advertiseId);
            return View(product);
        }
        public ActionResult ProductlistEdit(int advertiseId)
        {
            ProductListModel product = dataAccess.GetProductList(advertiseId);
            return View(product);
        }

        [HttpPost]
        public ActionResult ProductlistEdit(ProductListModel product)
        {
            try
            {
                TempData["AlertMessage"] = "Product-List Edited successfully......";
                dataAccess.UpdateProductList(product);
                return RedirectToAction(nameof(ProductList));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ProductListDelete(int advertiseId)
        {
            TempData["AlertMessage"] = "Product-List Data Deleted successfully......";
            ProductListModel product = dataAccess.GetProductList(advertiseId);
            return View(product);
        }

        [HttpPost]
        public ActionResult ProductListDelete(ProductListModel product)
        {
            try
            {
                dataAccess.DeleteProductList(product.advertiseId);
                return RedirectToAction(nameof(ProductList));
            }
            catch
            {
                return View();
            }
        }
    }
}