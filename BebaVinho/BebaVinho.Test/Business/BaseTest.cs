using BebaVinho.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Test.Business
{
    public class BaseTest
    {
        protected const string ADMIN_NAME_TEST = "Admin Teste Business";
        protected const string ADMIN_UPDATE_NAME_TEST = "Admin Update Business";

        protected const string USER_NAME_TEST = "Teste Business";
        protected const string USER_SURNAME_TEST = "Teste Business";
        protected const string USER_UPDATE_NAME_TEST = "Teste Business";
        protected const string USER_UPDATE_SURNAME_TEST = "Update Business";

        protected const string PRODUCT_NAME = "Teste Produto Business";
        protected const decimal PRODUCT_PRICE = 100;
        protected const string PRODUCT_DESCRIPTION = "Teste descrição produto Business";
        //TODO: Definir enumerador contendo os tipos de produto.
        protected const int PRODUCT_TYPE = 1;

        protected const string PRODUCT_UPDATE_NAME = "Teste Update Produto Business";
        protected const decimal PRODUCT_UPDATE_PRICE = 200;
        protected const string PRODUCT_UPDATE_DESCRIPTION = "Teste update descrição produto Business";
        //TODO: Definir enumerador contendo os tipos de produto.
        protected const int PRODUCT_UPDATE_TYPE = 2;

        protected IUserEntity<Domain.User> objEntityUser = null;
        protected ITestCase<Domain.User> objTestCaseUser = null;

        protected ICrudEntity<Domain.Admin> objEntityAdmin = null;
        protected ITestCase<Domain.Admin> objTestCaseAdmin = null;

        protected IAdminEntity<Domain.AdminProduct> objEntityAdminProduct = null;
        protected IAdminTestCase<Domain.AdminProduct> objTestCaseAdminProduct = null;

        protected IProductEntity<Domain.Product> objEntityProduct = null;
        protected IProductTestCase<Domain.Product> objTestCaseProduct = null;
    }
}
