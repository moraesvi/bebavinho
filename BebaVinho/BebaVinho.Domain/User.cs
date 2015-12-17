
namespace BebaVinho.Domain
{
    public class User
    {
        private int _id = 0;
        private string _name;
        private string _surname;
        private bool _isAdmin;

        public User(int id, string name, string surname, bool isAdmin)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _isAdmin = isAdmin;
        }

        public int Id 
        { 
            get 
            {
                return _id;
            } 
        }

        public string Name 
        {
            get 
            {
                return _name;
            } 
        }

        public string Surname 
        {
            get 
            {
                return _surname;
            } 
        }

        public bool IsAdmin
        {
            get
            {
                return _isAdmin;
            }
        }
    }
}
