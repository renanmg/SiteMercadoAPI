using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using SiteMercadoAPI.Domain.Interfaces;
using SiteMercadoAPI.Infra.Repository.Context;
using SiteMercadoAPI.Domain.Entities;
using System;

namespace SiteMercadoAPI.Infra.Repository.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly DataContext _context;

        public ProdutoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> Get()
        {
            return await _context.Produtos
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Produto> Get(Guid id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Produto produto)
        {
            _context.Entry<Produto>(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

    }
}