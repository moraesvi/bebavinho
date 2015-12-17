using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BebaVinho.MVC.Models.Product
{
    public class ShoppingCartModel
    {
        private List<ShoppingCart> _shoppingCartCollection;
        private List<ShoppingCart> _shoppingCartSession;
        private string _sessionKey;

        public ShoppingCartModel() 
        {
            _shoppingCartCollection = new List<ShoppingCart>();
        }

        public ShoppingCartModel(string sessionKey)
        {
            _shoppingCartCollection = new List<ShoppingCart>();
            _sessionKey = sessionKey;
        }

        public string SessionKey
        {
            get 
            {
                return _sessionKey;
            }
            set 
            {
                _sessionKey = value;
            }
        }

        public List<ShoppingCart> ShoppingCartCollection 
        {
            get 
            {
                return _shoppingCartCollection;
            }
        }

        public List<ShoppingCart> ShoppingCartSession 
        {
            get 
            {
                var session = HttpContext.Current.Session[_sessionKey];
                if(session == null) 
                {
                    return null;
                }
                return session as List<ShoppingCart>;
            }
        }

        public bool AddToCart(List<ShoppingCart> lstShoppingCart)
        {
            bool addToSession = false;
            _shoppingCartCollection = new List<ShoppingCart>();
            foreach (var shoppingCart in lstShoppingCart)
            {
                addToSession = Add(shoppingCart);
            }
            return addToSession;
        }

        public bool AddToCart(ShoppingCart shoppingCart) 
        {
            _shoppingCartCollection = new List<ShoppingCart>();
            return Add(shoppingCart);
        }

        private bool Add(ShoppingCart shoppingCart) 
        {
            _shoppingCartCollection.Add(shoppingCart);
            return true;
        }

        public bool RemoveToCart(ShoppingCart shoppingCart)
        {
            _shoppingCartCollection.Remove(shoppingCart);
            return true;
        }

        public bool RemoveToCartByProductId(int producdId) 
        {
            _shoppingCartCollection = GetShoppingCartSession();
            ShoppingCart shoppingCartProduct = _shoppingCartCollection.Where(value => value.ProductId == producdId)
                                                                      .FirstOrDefault();
            _shoppingCartCollection.Remove(shoppingCartProduct);
            return true;
        }

        public bool SaveSession(string sessionKey)
        {
            if (string.IsNullOrWhiteSpace(sessionKey))
            {
                throw new InvalidOperationException("Não foi definido um chave de sessão.");
            }

            HttpContext.Current.Session.Add(sessionKey, _shoppingCartCollection);
            return true;
        }

        public bool SaveSession()
        {
            if (string.IsNullOrWhiteSpace(_sessionKey))
            {
                throw new InvalidOperationException("Não foi definido um chave de sessão.");
            }

            if (HttpContext.Current.Session[_sessionKey] != null)
                HttpContext.Current.Session.Remove(_sessionKey);

            HttpContext.Current.Session.Add(_sessionKey, _shoppingCartCollection);
            return true;
        }

        public bool ClearSession()
        {
            if (string.IsNullOrWhiteSpace(_sessionKey))
            {
                throw new InvalidOperationException("Não foi definido um chave de sessão.");
            }

            if (HttpContext.Current.Session[_sessionKey] != null)
                HttpContext.Current.Session.Remove(_sessionKey);

            return true;
        }

        private List<ShoppingCart> GetShoppingCartSession() 
        {
            return HttpContext.Current.Session[_sessionKey] as List<ShoppingCart>;
        }

        private List<ShoppingCart> GetShoppingCartSession(string sessionKey)
        {
            return HttpContext.Current.Session[sessionKey] as List<ShoppingCart>;
        }
    }

    public class ShoppingCart 
    {
        public int ProductId { get; set; }

        public int Count { get; set; }

        public DateTime BuyDate { get; set; }
    }
}