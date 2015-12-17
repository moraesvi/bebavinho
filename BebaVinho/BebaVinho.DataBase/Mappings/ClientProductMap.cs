using BebaVinho.DataBase.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BebaVinho.DataBase.Mappings
{
    public class ClientProductMap : EntityTypeConfiguration<ClientProduct>
    {
        public ClientProductMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("ClientProduct");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ClientId).HasColumnName("ClientId");
            Property(t => t.ProductId).HasColumnName("ProductId");
            Property(t => t.RegisterDate).HasColumnName("RegisterDate");
            Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            HasRequired(t => t.Client)
                .WithMany(t => t.ClientProducts)
                .HasForeignKey(d => d.ClientId);
            HasRequired(t => t.Product)
                .WithMany(t => t.ClientProducts)
                .HasForeignKey(d => d.ProductId);

        }
    }
}
