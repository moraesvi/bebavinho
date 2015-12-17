using BebaVinho.DataBase.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BebaVinho.DataBase.Mappings
{
    public class LogHistoryMap : EntityTypeConfiguration<LogHistory>
    {
        public LogHistoryMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Descritption)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            ToTable("LogHistory");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Descritption).HasColumnName("Descritption");
            Property(t => t.Type).HasColumnName("Type");
            Property(t => t.RegisterDate).HasColumnName("RegisterDate");
            Property(t => t.UpdateDate).HasColumnName("UpdateDate");
        }
    }
}
