
namespace BebaVinho.Domain
{
    public class AdminDeliver
    {
        public int Id { get; set; }

        public int AdminId { get; set; }

        public Admin Admin { get; set; }
    }
}
