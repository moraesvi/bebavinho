using BebaVinho.DataBase.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BebaVinho.DataBase.Mappings
{
    public class AdminProductMap : EntityTypeConfiguration<AdminProduct>
    {
        public AdminProductMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("AdminProduct");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IsActive).HasColumnName("IsActive");
            Property(t => t.AdminId).HasColumnName("AdminId");
            Property(t => t.RegisterDate).HasColumnName("RegisterDate");
            Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            HasOptional(t => t.Admin)
                .WithMany(t => t.AdminProducts)
                .HasForeignKey(d => d.AdminId);

        }
    }
}
