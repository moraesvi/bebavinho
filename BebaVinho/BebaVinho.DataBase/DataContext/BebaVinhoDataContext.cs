using BebaVinho.DataBase.Mappings;
using BebaVinho.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.DataBase.DataContext
{
    public class BebaVinhoDataContext : DbContext
    {
        static BebaVinhoDataContext()
        {
            Database.SetInitializer<BebaVinhoDataContext>(null);
        }

        public BebaVinhoDataContext()
            : base("Name=BebaVinhoEntities")
        {
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<AdminClient> AdminClient { get; set; }
        public DbSet<AdminDeliver> AdminDeliver { get; set; }
        public DbSet<AdminProduct> AdminProduct { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientLogHistory> ClientLogHistory { get; set; }
        public DbSet<ClientProduct> ClientProduct { get; set; }
        public DbSet<Deliver> Deliver { get; set; }
        public DbSet<LogHistory> LogHistory { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdminMap());
            modelBuilder.Configurations.Add(new AdminClientMap());
            modelBuilder.Configurations.Add(new AdminDeliverMap());
            modelBuilder.Configurations.Add(new AdminProductMap());
            modelBuilder.Configurations.Add(new ClientMap());
            modelBuilder.Configurations.Add(new ClientLogHistoryMap());
            modelBuilder.Configurations.Add(new ClientProductMap());
            modelBuilder.Configurations.Add(new DeliverMap());
            modelBuilder.Configurations.Add(new LogHistoryMap());
            modelBuilder.Configurations.Add(new LoginMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductDetailsMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
