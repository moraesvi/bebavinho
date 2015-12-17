using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BebaVinho.Infrastructure.P.O.C.O;
using BebaVinho.Infrastructure;
using BebaVinho.Domain.Interface;

namespace BebaVinho.Test.Infrastructure
{
    [TestClass]
    public class UserTest : BaseTest
    {
        [TestInitialize]
        public void Initialize() 
        {
            objEntityUser = new User();
            objTestCaseUser = new User();
            InsertUserTest();
        }

        private void InsertUserTest() 
        {
            Domain.User objUser = new Domain.User(0, USER_NAME_TEST, USER_SURNAME_TEST, true);
            objEntityUser.AddOrUpdate(objUser);
        }

        private void DeleteUserTest()
        {
            objTestCaseUser.DeleteDataFromDataBase(USER_NAME_TEST + " " + USER_SURNAME_TEST);
            objTestCaseUser.DeleteDataFromDataBase(USER_UPDATE_NAME_TEST + " " + USER_UPDATE_SURNAME_TEST);
        }

        [TestMethod]
        [TestCategory("User")]
        public void Validar_Insercao_De_Usuario_Test()
        {
            DeleteUserTest();
            Domain.User objUser = new Domain.User(0, USER_NAME_TEST, USER_SURNAME_TEST, true);
            int userId = objEntityUser.AddOrUpdate(objUser);
            var result = objEntityUser.GetById(userId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("User")]
        public void Buscar_Usuarios_Test()
        {
            var resultado = objEntityUser.GetByName(USER_NAME_TEST + " " + USER_SURNAME_TEST);
            Assert.IsInstanceOfType(resultado, typeof(Domain.User));
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        [TestCategory("User")]
        public void Buscar_Usuarios_Pelo_Id_Test()
        {
            var objUser = objEntityUser.GetByName(USER_NAME_TEST + " " + USER_SURNAME_TEST);
            var resultado = objEntityUser.GetById(objUser.Id);
            Assert.IsNotInstanceOfType(resultado, typeof(IEnumerable<Domain.User>));
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        [TestCategory("User")]
        public void Validar_Remocao_De_Usuario_Test()
        {
            var objUser = objEntityUser.GetByName(USER_NAME_TEST + " " + USER_SURNAME_TEST);
            bool removeResult = objEntityUser.Remove(objUser);
            var result = objEntityUser.GetById(objUser.Id);
            Assert.IsTrue(removeResult);
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("User")]
        public void Validar_Remocao_Perfil_Administrador_Do_Usuario_Test()
        {
            var objUser = objEntityUser.GetByName(USER_NAME_TEST + " " + USER_SURNAME_TEST);
            bool removeResult = objEntityUser.RemoveAdminProfile(objUser.Id);
            var result = objEntityUser.GetById(objUser.Id);
            Assert.IsTrue(removeResult);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsAdmin, false);
        }

        [TestMethod]
        [TestCategory("User")]
        public void Validar_Atualizacao_De_Usuario_Test()
        {
            var objUser = objEntityUser.GetByName(USER_NAME_TEST + " " + USER_SURNAME_TEST);
            var objUserUpdate = new Domain.User(objUser.Id, USER_UPDATE_NAME_TEST, USER_UPDATE_SURNAME_TEST, false);
            objUser = objEntityUser.AddOrUpdateAndGetEntity(objUserUpdate);
            Assert.AreEqual(objUser.Id, objUserUpdate.Id);
            Assert.AreEqual(objUser.Name, objUserUpdate.Name);
            Assert.AreEqual(objUser.Surname, objUserUpdate.Surname);
            Assert.AreEqual(objUser.IsAdmin, objUserUpdate.IsAdmin);
        }

        [TestCleanup]
        public void TestCleanup() 
        {
            DeleteUserTest();
        }
    }
}
