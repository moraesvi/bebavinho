using BebaVinho.DataBase.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BebaVinho.DataBase.Mappings
{
    public class ClientMap : EntityTypeConfiguration<Client>
    {
        public ClientMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Surname)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.ContactPhone)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            ToTable("Client");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Surname).HasColumnName("Surname");
            Property(t => t.ContactPhone).HasColumnName("ContactPhone");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.ReceiveProductUpdates).HasColumnName("ReceiveProductUpdates");
            Property(t => t.AdminClientId).HasColumnName("AdminClientId");
            Property(t => t.RegisterDate).HasColumnName("RegisterDate");
            Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
             HasRequired(t => t.AdminClient)
                .WithMany(t => t.Clients)
                .HasForeignKey(d => d.AdminClientId);

        }
    }
}
