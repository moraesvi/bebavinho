using BebaVinho.DataBase.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BebaVinho.DataBase.Mappings
{
    public class ClientLogHistoryMap : EntityTypeConfiguration<ClientLogHistory>
    {
        public ClientLogHistoryMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("ClientLogHistory");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.EmailSent).HasColumnName("EmailSent");
            Property(t => t.LogHistoryId).HasColumnName("LogHistoryId");
            Property(t => t.ClientId).HasColumnName("ClientId");

            // Relationships
            HasRequired(t => t.Client)
                .WithMany(t => t.ClientLogHistories)
                .HasForeignKey(d => d.ClientId);
            HasRequired(t => t.LogHistory)
                .WithMany(t => t.ClientLogHistories)
                .HasForeignKey(d => d.LogHistoryId);

        }
    }
}
