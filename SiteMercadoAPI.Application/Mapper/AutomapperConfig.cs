using AutoMapper;
using SiteMercadoAPI.Application.Models;
using SiteMercadoAPI.Domain.Entities;

namespace SiteMercadoAPI.Application.Mapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Produto, ProdutoModel>().ReverseMap();
        }
    }
}