using BebaVinho.DataBase.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BebaVinho.DataBase.Mappings
{
    public class AdminDeliverMap : EntityTypeConfiguration<AdminDeliver>
    {
        public AdminDeliverMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("AdminDeliver");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IsActive).HasColumnName("IsActive");
            Property(t => t.AdminId).HasColumnName("AdminId");
            Property(t => t.RegisterDate).HasColumnName("RegisterDate");
            Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            HasRequired(t => t.Admin)
                .WithMany(t => t.AdminDelivers)
                .HasForeignKey(d => d.AdminId);

        }
    }
}
