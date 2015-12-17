using BebaVinho.DataBase.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BebaVinho.DataBase.Mappings
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(200);

            Property(t => t.Region)
                .IsRequired()
                .HasMaxLength(200);

            Property(t => t.Descritption)
                .IsRequired()
                .HasMaxLength(500);

            Property(t => t.ImagePath)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            ToTable("Product");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.OldPrice).HasColumnName("OldPrice");
            Property(t => t.Price).HasColumnName("Price");
            Property(t => t.Region).HasColumnName("Region");
            Property(t => t.Descritption).HasColumnName("Descritption");
            Property(t => t.ImagePath).HasColumnName("ImagePath");
            Property(t => t.Type).HasColumnName("Type");
            Property(t => t.IsActive).HasColumnName("IsActive");
            Property(t => t.AdminProductId).HasColumnName("AdminProductId");
            Property(t => t.RegisterDate).HasColumnName("RegisterDate");
            Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            HasOptional(t => t.AdminProduct)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.AdminProductId);

        }
    }
}
