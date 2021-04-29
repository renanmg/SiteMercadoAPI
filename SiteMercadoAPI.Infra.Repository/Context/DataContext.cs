using Microsoft.EntityFrameworkCore;
using SiteMercadoAPI.Domain.Entities;
using SiteMercadoAPI.Infra.Repository.Maps;

namespace SiteMercadoAPI.Infra.Repository.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=produtos;User ID=SA;Password=eUR3yMdZ");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProdutoMap());
        }
    }
}