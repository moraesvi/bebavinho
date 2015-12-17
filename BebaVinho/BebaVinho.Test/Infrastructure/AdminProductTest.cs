using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BebaVinho.Infrastructure;

namespace BebaVinho.Test.Infrastructure
{
    [TestClass]
    public class AdminProductTest : BaseTest
    {
        [TestInitialize]
        public void Initialize()
        {
            objEntityUser = new User();
            objEntityAdmin = new Admin();
            objEntityAdminProduct = new AdminProduct();
            objTestCaseAdminProduct = new AdminProduct();
            objTestCaseAdmin = new Admin();
            objTestCaseUser = new User();
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

            lstAdminProduct.ForEach(objAdminProduct =>
            {
                objTestCaseAdminProduct.DeleteDataFromDataBase(objAdminProduct.Id);
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

        private Domain.AdminProduct InsertAdminProduct() 
        {
            Domain.Admin objAdmin = GetAdminObject(false);
            Domain.AdminProduct objAdminProduct = new Domain.AdminProduct()
            {
                Id = 0,
                AdminId = objAdmin.Id,
                Admin = objAdmin
            };

            return objEntityAdminProduct.AddOrUpdateAndGetEntity(objAdminProduct);
        }

        [TestMethod]
        [TestCategory("AdminProduct")]
        [ExpectedException(typeof(InvalidOperationException), "Erro ao inserir o perfil 'administrador de produtos', administrador não encontrado.")]
        public void Validar_Insercao_Do_Perfil_Admin_De_Produtos_Invalido_Sem_Admin_Cadastrado_Test()
        {
            Domain.AdminProduct objAdminProduct = new Domain.AdminProduct()
            {
                Id = 0
            };
            objEntityAdminProduct.AddOrUpdate(objAdminProduct);
        }

        [TestMethod]
        [TestCategory("AdminProduct")]
        public void Validar_Insercao_Do_Perfil_Admin_De_Produtos_Test()
        {
            Domain.Admin objAdmin = GetAdminObject();
            Domain.AdminProduct objAdminProduct = new Domain.AdminProduct()
            {
                Id = 0,
                AdminId = objAdmin.Id
            };
            int adminProductId = objEntityAdminProduct.AddOrUpdate(objAdminProduct);
            var result = objEntityAdminProduct.GetById(adminProductId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("AdminProduct")]
        public void Validar_Remocao_Do_Perfil_Admin_de_Produtos_Test()
        {
            InsertAdminProduct();
            Domain.Admin objAdmin = objEntityAdmin.GetByName(ADMIN_NAME_TEST);
            bool result = objEntityAdminProduct.RemoveProfile(objAdmin.Id);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("AdminProduct")]
        public void Validar_Ativacao_Do_Perfil_Admin_de_Produtos_Test()
        {
            InsertAdminProduct();
            Domain.Admin objAdmin = objEntityAdmin.GetByName(ADMIN_NAME_TEST);
            bool removed = objEntityAdminProduct.RemoveProfile(objAdmin.Id);
            if (removed)
            {
                bool result = objEntityAdminProduct.ActiveProfile(objAdmin.Id);
                Assert.IsTrue(result);
                return;
            }
            Assert.Inconclusive();
        }

        private Domain.Admin GetAdminObject(bool active = true)
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

            return objEntityAdmin.AddOrUpdateAndGetEntity(objAdmin);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteAdminProductTest();
            DeleteAdminTest();
            DeleteUserTest();
        }
    }
}
