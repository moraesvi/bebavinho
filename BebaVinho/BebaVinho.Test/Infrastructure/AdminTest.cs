using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BebaVinho.Domain.Interface;
using BebaVinho.Infrastructure;

namespace BebaVinho.Test.Infrastructure
{
    [TestClass]
    public class AdminTest : BaseTest
    {
        [TestInitialize]
        public void Initialize()
        {
            objEntityAdmin = new Admin();
            objTestCaseAdmin = new Admin();
            objTestCaseUser = new User();
            objEntityUser = new User();
        }

        private void InsertUserTest(bool admin)
        {
            Domain.User objUser = new Domain.User(0, USER_NAME_TEST, USER_SURNAME_TEST, admin);
            objEntityUser.AddOrUpdate(objUser);
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

        [TestMethod]
        [TestCategory("Admin")]
        [ExpectedException(typeof(InvalidOperationException), "Erro ao inserir o adminisrador, usuário não encontrado.")]
        public void Validar_Insercao_De_Admin_Invalido_Sem_Usuario_Test()
        {
            Domain.Admin objAdmin = new Domain.Admin()
            {
                Id = 0,
                Status = "Produtos",
                FullName = ADMIN_NAME_TEST
            };
            objEntityAdmin.AddOrUpdate(objAdmin);
        }

        [TestMethod]
        [TestCategory("Admin")]
        public void Validar_Insercao_De_Admin_Valido_Com_Usuario_Test()
        {
            InsertUserTest(true);
            Domain.Admin objAdmin = GetAdminObject(ADMIN_NAME_TEST);
            int adminId = objEntityAdmin.AddOrUpdate(objAdmin);
            var result = objEntityAdmin.GetById(adminId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Admin")]
        [ExpectedException(typeof(InvalidOperationException), "Erro ao inserir o adminisrador, o usuário necessita ser administrador.")]
        public void Validar_Insercao_De_Admin_Valido_Usuario_Nao_E_Administrador_Test()
        {
            InsertUserTest(false);
            Domain.Admin objAdmin = GetAdminObject(ADMIN_NAME_TEST);
            objEntityAdmin.AddOrUpdate(objAdmin);
        }

        [TestMethod]
        [TestCategory("Admin")]
        public void Validar_Remocao_De_Um_Administrador_Test()
        {
            InsertUserTest(true);
            Domain.Admin objAdmin = GetAdminObject(ADMIN_NAME_TEST);
            objAdmin = objEntityAdmin.AddOrUpdateAndGetEntity(objAdmin);
            objEntityAdmin.Remove(objAdmin);

            var result = objEntityAdmin.GetById(objAdmin.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("Admin")]
        public void Validar_Atualizacao_Dos_Dados_De_Um_Administrador_Test()
        {
            InsertUserTest(true);
            Domain.Admin objAdmin = GetAdminObject(ADMIN_UPDATE_NAME_TEST);
            objEntityAdmin.AddOrUpdate(objAdmin);

            objAdmin = objEntityAdmin.GetByName(ADMIN_UPDATE_NAME_TEST);
            var objUser = objEntityUser.GetByName(USER_NAME_TEST + " " + USER_SURNAME_TEST);
            Domain.Admin objAdminUpdate = new Domain.Admin()
            {
                Id = objAdmin.Id,
                Status = "Distribuicao",
                FullName = ADMIN_UPDATE_NAME_TEST,
                User = objUser,
                UserId = objUser.Id
            };

            objAdmin = objEntityAdmin.AddOrUpdateAndGetEntity(objAdminUpdate);

            //TODO: Definir enumerador de status.
            string formatedStatus = objAdmin.Status == "1" ? "Distribuicao" : string.Empty;

            Assert.AreEqual(objAdmin.Id, objAdminUpdate.Id);
            Assert.AreEqual(formatedStatus, objAdminUpdate.Status);
            Assert.AreEqual(objAdmin.FullName, objAdminUpdate.FullName);
            Assert.AreEqual(objAdmin.UserId, objAdminUpdate.UserId);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteUserTest();
            DeleteAdminTest();
        }

        private Domain.Admin GetAdminObject(string fullname) 
        {
            var objUser = objEntityUser.GetByName(USER_NAME_TEST + " " + USER_SURNAME_TEST);
            Domain.Admin objAdmin = new Domain.Admin()
            {
                Id = 0,
                Status = "Produtos",
                FullName = fullname,
                User = objUser,
                UserId = objUser.Id
            };

            return objAdmin;
        }
    }
}
