
namespace BebaVinho.Domain
{
    public class Admin
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Status { get; set; }

        public int? Count { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
