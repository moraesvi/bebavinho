using BebaVinho.DataBase.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BebaVinho.DataBase.Mappings
{
    public class AdminMap : EntityTypeConfiguration<Admin>
    {
        public AdminMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            ToTable("Admin");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.FullName).HasColumnName("FullName");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.Count).HasColumnName("Count");
            Property(t => t.IsActive).HasColumnName("IsActive");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.RegisterDate).HasColumnName("RegisterDate");
            Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            HasRequired(t => t.User)
                .WithMany(t => t.Admins)
                .HasForeignKey(d => d.UserId);

        }
    }
}
