using BebaVinho.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.DataBase.Mappings
{
    public class ProductDetailsMap : EntityTypeConfiguration<ProductDetails>
    {
        public ProductDetailsMap() 
        {
            HasKey(t => t.Id);

            ToTable("ProductDetails");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.SmallDetail).HasColumnName("SmallDetail");
            Property(t => t.Detail).HasColumnName("Detail");
            Property(t => t.IsActive).HasColumnName("IsActive");
            Property(t => t.ImagePath1).HasColumnName("ImagePath1");
            Property(t => t.ImagePath2).HasColumnName("ImagePath2");
            Property(t => t.ProductId).HasColumnName("ProductId");

            Property(t => t.SmallDetail)
                    .IsRequired()
                    .HasMaxLength(300);

            Property(t => t.Detail)
                    .IsRequired()
                    .HasMaxLength(500);

            Property(t => t.ImagePath1)
                    .IsRequired()
                    .HasMaxLength(150);

            Property(t => t.ImagePath2)
                    .IsRequired()
                    .HasMaxLength(150);

            HasOptional(t => t.Product)
                    .WithOptionalDependent(t => t.ProductDetails);
        }
    }
}
