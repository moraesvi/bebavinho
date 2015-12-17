using BebaVinho.DataBase.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BebaVinho.DataBase.Mappings
{
    public class AdminClientMap : EntityTypeConfiguration<AdminClient>
    {
        public AdminClientMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("AdminClient");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IsActive).HasColumnName("IsActive");
            Property(t => t.AdminId).HasColumnName("AdminId");
            Property(t => t.RegisterDate).HasColumnName("RegisterDate");
            Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            HasRequired(t => t.Admin)
                .WithMany(t => t.AdminClients)
                .HasForeignKey(d => d.AdminId);

        }
    }
}
