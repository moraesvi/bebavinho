using BebaVinho.MVC.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BebaVinho.MVC.Controllers
{
    public class DefaultController : Controller
    {
        private ProductCrudModel objProductCrudModel = null;
        private ShoppingCartModel objShoppingCartModel = null;

        public DefaultController() 
        {
            objProductCrudModel = new ProductCrudModel();
            objShoppingCartModel = new ShoppingCartModel();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProducts() 
        {
            var data = objProductCrudModel.GetProducts();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductsWithPathImage()
        {
            try
            {
                var json = new { data = objProductCrudModel.GetProductsWithPathImage(), message = ""};
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
                var json = new { data = session, message = "" };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var json = new { data = "", message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveShoppingCartSession(ShoppingCart shoppingCart) 
        {
            try
            {
                objShoppingCartModel.SessionKey = "ShoppingCart";
                var added = objShoppingCartModel.AddToCart(shoppingCart);
                objShoppingCartModel.SaveSession();
                var json = new { data = added, message = "" };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var json = new { data = "", message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Sobre() 
        {
            return View();
        }

        public ActionResult Contato()
        {
            return View();
        }
    }
}