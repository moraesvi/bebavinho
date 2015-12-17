using BebaVinho.MVC.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BebaVinho.MVC.Controllers
{
    public class NewController : Controller
    {
        private ProductCrudModel objProductCrudModel = null;
        private ShoppingCartModel objShoppingCartModel = null;

        public NewController()
        {
            objProductCrudModel = new ProductCrudModel();
            objShoppingCartModel = new ShoppingCartModel();
        }
        // GET: New
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }


        public JsonResult GetProducts()
        {
            try
            {
                var data = objProductCrudModel.GetProducts();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var json = new { data = "", message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProductProductDetailsById(int id)
        {
            try
            {
                var json = new { data = objProductCrudModel.GetProductProductDetailsById(id), message = "" };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var json = new { data = "", message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetByIds(int [] ids)
        {
            try
            {
                var json = new { data = objProductCrudModel.GetByIds(ids), message = "" };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var json = new { data = "", message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProductsWithPathImage()
        {
            try
            {
                var json = new { data = objProductCrudModel.GetProductsWithPathImage(), message = "" };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var json = new { data = "", message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProductsByPagination(int skip)
        {
            try
            {
                int take = 8;
                var products = objProductCrudModel.GetByPagination(skip, take);
                var json = new { data = products, message = "" };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var json = new { data = "", message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetShoppingCartSession()
        {
            try
            {
                objShoppingCartModel.SessionKey = "ShoppingCart";
                var session = objShoppingCartModel.ShoppingCartSession;
                var jsonEmpty = new { data = session, message = "" };
                return Json(jsonEmpty, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var json = new { data = "", message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveShoppingCartSession(List<ShoppingCart> arrayShoppingCart)
        {
            try
            {
                objShoppingCartModel.SessionKey = "ShoppingCart";
                var added = objShoppingCartModel.AddToCart(arrayShoppingCart);
                objShoppingCartModel.SaveSession();
                var json = new { data = added, message = "" };
                return Json(json);
            }
            catch (Exception ex)
            {
                var json = new { data = "", message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ClearShoppingCartSession()
        {
            try
            {
                objShoppingCartModel.SessionKey = "ShoppingCart";
                var removed = objShoppingCartModel.ClearSession();
                var json = new { data = removed, message = "" };
                return Json(json);
            }
            catch (Exception ex)
            {
                var json = new { data = "", message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }
    }
}