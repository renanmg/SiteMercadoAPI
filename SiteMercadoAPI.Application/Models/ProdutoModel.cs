using System;

namespace SiteMercadoAPI.Application.Models
{
    public class ProdutoModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }   
        public string Imagem { get; set; }
        
    }
}