using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteMercadoAPI.Domain.Entities;

namespace SiteMercadoAPI.Infra.Repository.Maps
{
    public class ProdutoMap: IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(x=> x.Id);
            builder.Property(x=> x.Imagem).HasMaxLength(1024).HasColumnType("varchar(1024)");
            builder.Property(x=> x.Valor).IsRequired().HasColumnType("money");
            builder.Property(x=> x.Nome).IsRequired().HasMaxLength(120).HasColumnType("varchar(120)");
        }
    }
}