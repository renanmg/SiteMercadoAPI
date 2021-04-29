

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiteMercadoAPI.Domain.Entities;

namespace SiteMercadoAPI.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> Get();
        Task<Produto> Get(Guid id);
        Task Save(Produto produto);
        Task Update(Produto produto);
        Task Delete(Produto produto);
    }
}