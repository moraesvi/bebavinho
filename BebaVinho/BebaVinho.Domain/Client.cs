
namespace BebaVinho.Domain
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ContactPhone { get; set; }

        public string Email { get; set; }

        public bool ReceiveProductUpdates { get; set; }

        public int AdminClientId { get; set; }

        public AdminClient AdminClient { get; set; }
    }
}
