using BebaVinho.DataBase.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BebaVinho.DataBase.Mappings
{
    public class DeliverMap : EntityTypeConfiguration<Deliver>
    {
        public DeliverMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            ToTable("Deliver");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.IsActive).HasColumnName("IsActive");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.Type).HasColumnName("Type");
            Property(t => t.AdminDeliverId).HasColumnName("AdminDeliverId");
            Property(t => t.RegisterDate).HasColumnName("RegisterDate");
            Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            HasOptional(t => t.AdminDeliver)
                .WithMany(t => t.Delivers)
                .HasForeignKey(d => d.AdminDeliverId);

        }
    }
}
