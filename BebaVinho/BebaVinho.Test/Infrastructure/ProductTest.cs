using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BebaVinho.Infrastructure;

namespace BebaVinho.Test.Infrastructure
{
    [TestClass]
    public class ProductTest : BaseTest
    {
        private Domain.AdminProduct _objAdminProduct = null;

        [TestInitialize]
        public void Initialize()
        {
            objEntityUser = new User();
            objEntityAdmin = new Admin();
            objEntityAdminProduct = new AdminProduct();
            objTestCaseAdminProduct = new AdminProduct();
            objTestCaseAdmin = new Admin();
            objTestCaseUser = new User();
            objEntityProduct = new Product();
            objTestCaseProduct = new Product();

            _objAdminProduct = GetAdminProductObject();
        }

        private Domain.Product InsertAndGetProduct()
        {
            Domain.Product objProduct = new Domain.Product()
            {
                Id = 0,
                Name = PRODUCT_NAME,
                Price = PRODUCT_PRICE,
                Type = PRODUCT_TYPE,
                Description = PRODUCT_DESCRIPTION,
                AdminProduct = _objAdminProduct
            };

            return objEntityProduct.AddOrUpdateAndGetEntity(objProduct);
        }

        private void DeleteProductTest()
        {
            objTestCaseProduct.DeleteDataFromDataBase(PRODUCT_NAME);
            objTestCaseProduct.DeleteDataFromDataBase(PRODUCT_UPDATE_NAME);
        }

        private void DeleteAdminProductTest()
        {
            var objAdmin = objEntityAdmin.GetByName(ADMIN_NAME_TEST);
            var objAdminUpdate = objEntityAdmin.GetByName(ADMIN_UPDATE_NAME_TEST);

            int adminId = 0;

            if (objAdmin != null)
                adminId = objAdmin.Id;
            if (objAdminUpdate != null)
                adminId = objAdminUpdate.Id;

            if (adminId == 0)
                return;

            var lstAdminProduct = objTestCaseAdminProduct.GetByAdminId(adminId);

            lstAdminProduct.ForEach(obj =>
            {
                objTestCaseAdminProduct.DeleteDataFromDataBase(obj.Id);
            });
        }

        private void DeleteAdminTest()
        {
            objTestCaseAdmin.DeleteDataFromDataBase(ADMIN_NAME_TEST);
            objTestCaseAdmin.DeleteDataFromDataBase(ADMIN_UPDATE_NAME_TEST);
        }

        private void DeleteUserTest()
        {
            objTestCaseUser.DeleteDataFromDataBase(USER_NAME_TEST + " " + USER_SURNAME_TEST);
            objTestCaseUser.DeleteDataFromDataBase(USER_UPDATE_NAME_TEST + " " + USER_UPDATE_SURNAME_TEST);
        }

        private Domain.AdminProduct GetAdminProductObject()
        {
            Domain.User objUser = new Domain.User(0, USER_NAME_TEST, USER_SURNAME_TEST, true);
            objEntityUser.AddOrUpdate(objUser);

            objUser = objEntityUser.GetByName(USER_NAME_TEST + " " + USER_SURNAME_TEST);
            Domain.Admin objAdmin = new Domain.Admin()
            {
                Id = 0,
                Status = "Produtos",
                FullName = ADMIN_NAME_TEST,
                User = objUser,
                UserId = objUser.Id
            };

            objAdmin = objEntityAdmin.AddOrUpdateAndGetEntity(objAdmin);

            Domain.AdminProduct objAdminProduct = new Domain.AdminProduct()
            {
                Id = 0,
                AdminId = objAdmin.Id,
                Admin = objAdmin
            };

            return objEntityAdminProduct.AddOrUpdateAndGetEntity(objAdminProduct);
        }

        [TestMethod]
        public void Validar_Insercao_De_Um_Novo_Produto_Valido_Test()
        {
            Domain.Product objProduct = new Domain.Product()
            {
                Id = 0,
                Name = PRODUCT_NAME,
                Price = PRODUCT_PRICE,
                Type = PRODUCT_TYPE,
                Description = PRODUCT_DESCRIPTION,
                AdminProductId = _objAdminProduct.Id
            };

            var idProduct = objEntityProduct.AddOrUpdate(objProduct);
            objProduct = objEntityProduct.GetById(idProduct);
            Assert.IsNotNull(objProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Erro ao inserir um novo produto, administrador não encontrado.")]
        public void Validar_Insercao_De_Um_Novo_Produto_Invalido_Sem_Admin_Test()
        {
            Domain.Product objProduct = new Domain.Product()
            {
                Id = 0,
                Name = PRODUCT_NAME,
                Price = PRODUCT_PRICE,
                Type = PRODUCT_TYPE,
                Description = PRODUCT_DESCRIPTION,
                AdminProductId = null
            };

            objEntityProduct.AddOrUpdate(objProduct);
        }

        [TestMethod]
        public void Validar_Atualizacao_De_Um_Produto_Test()
        {
            Domain.Product objProduct = InsertAndGetProduct();

            objProduct.Name = PRODUCT_UPDATE_NAME;
            objProduct.Price = PRODUCT_UPDATE_PRICE;
            objProduct.Type = PRODUCT_UPDATE_TYPE;
            objProduct.Description = PRODUCT_UPDATE_DESCRIPTION;

            var result = objEntityProduct.AddOrUpdateAndGetEntity(objProduct);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, objProduct.Name);
            Assert.AreEqual(result.Price, objProduct.Price);
            Assert.AreEqual(result.Type, objProduct.Type);
            Assert.AreEqual(result.Description, objProduct.Description);
        }

        [TestMethod]
        public void Validar_Busca_De_Produtos_Test()
        {
            InsertAndGetProduct();
            InsertAndGetProduct();
            var result = objEntityProduct.Get;
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Count(), 0);
        }

        [TestMethod]
        public void Validar_Busca_Do_Produto_Pelo_Id_Test()
        {
            Domain.Product objProduct = new Domain.Product()
            {
                Id = 0,
                Name = PRODUCT_NAME,
                Price = PRODUCT_PRICE,
                Type = PRODUCT_TYPE,
                Description = PRODUCT_DESCRIPTION,
                AdminProductId = _objAdminProduct.Id
            };

            int productId = objEntityProduct.AddOrUpdate(objProduct);
            objProduct = objEntityProduct.GetById(productId);
            Assert.IsNotNull(objProduct);
        }

        [TestMethod]
        public void Validar_Busca_Do_Produto_Por_Pagina()
        {
            Domain.Product objProduct = new Domain.Product()
            {
                Id = 0,
                Name = PRODUCT_NAME,
                Price = PRODUCT_PRICE,
                Type = PRODUCT_TYPE,
                Description = PRODUCT_DESCRIPTION,
                AdminProductId = _objAdminProduct.Id
            };

            int productId = objEntityProduct.AddOrUpdate(objProduct);
            int skip = 1;
            int take = 10;
            IEnumerable<Domain.Procedure.PrGetProducts> lstPrGetProducts = objEntityProduct.GetByPagination(skip, take);
            Assert.IsNotNull(lstPrGetProducts);
            Assert.AreNotEqual(lstPrGetProducts.Count(), 0);
        }

        [TestMethod]
        public void Validar_Busca_Do_Produto_Pelo_Nome_Test()
        {
            Domain.Product objProduct = InsertAndGetProduct();
            var result = objEntityProduct.GetByName(objProduct.Name);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Count(), 0);
        }

        [TestMethod]
        public void Validar_Busca_Do_Produto_Pelo_Tipo()
        {
            Domain.Product objProduct = InsertAndGetProduct();
            var result = objEntityProduct.GetByType(objProduct.Type);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Count(), 0);
        }

        [TestMethod]
        public void Validar_Busca_Do_Produto_Pelo_Preco_Test()
        {
            Domain.Product objProduct = InsertAndGetProduct();
            var result = objEntityProduct.GetByPrice(objProduct.Price);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Count(), 0);
        }

        [TestMethod]
        public void Validar_Remocao_De_Um_Produto_Test()
        {
            Domain.Product objProduct = InsertAndGetProduct();
            objEntityProduct.Remove(objProduct);
            var result = objEntityProduct.GetById(objProduct.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Validar_Desativacao_De_Um_Produto_Test()
        {
            Domain.Product objProduct = InsertAndGetProduct();
            bool result = objEntityProduct.Disable(objProduct.Id);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validar_Ativacao_De_Um_Produto_Test()
        {
            Domain.Product objProduct = InsertAndGetProduct();
            objEntityProduct.Disable(objProduct.Id);
            bool result = objEntityProduct.Enable(objProduct.Id);
            Assert.IsTrue(result);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteProductTest();
            DeleteAdminProductTest();
            DeleteAdminTest();
            DeleteUserTest();
        }
    }
}
