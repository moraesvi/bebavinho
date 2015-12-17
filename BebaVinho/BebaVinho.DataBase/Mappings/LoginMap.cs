using BebaVinho.DataBase.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BebaVinho.DataBase.Mappings
{
    public class LoginMap : EntityTypeConfiguration<Login>
    {
        public LoginMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(500);

            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            ToTable("Login");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Password).HasColumnName("Password");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.IsLogged).HasColumnName("IsLogged");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.RegisterDate).HasColumnName("RegisterDate");
            Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            HasRequired(t => t.User)
                .WithMany(t => t.Logins)
                .HasForeignKey(d => d.UserId);

        }
    }
}
