using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.ViewModels;
using SiteMercadoAPI.Application.Models;
using SiteMercadoAPI.Domain.Entities;
using SiteMercadoAPI.Domain.Interfaces;

namespace SiteMercadoAPI.Application.Controllers
{
    [ApiController]
    [Route("v1/produtos")]
    public class ProdutoController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<ProdutoModel>>> Get([FromServices] IProdutoRepository repository, [FromServices] IMapper mapper)
        {

            return mapper.Map<List<ProdutoModel>>(await repository.Get());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult<ProdutoModel>> Get([FromServices] IProdutoRepository repository, [FromServices] IMapper mapper, Guid id)
        {
            return mapper.Map<ProdutoModel>(await repository.Get(id));
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ResultViewModel>> Post([FromServices] IProdutoRepository repository, [FromServices] IMapper mapper, [FromBody] ProdutoModel model)
        {

            var produto = mapper.Map<Produto>(model);
            produto.Validate();

            if (produto.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível inserir o produto",
                    Data = produto.Notifications
                };

            if (produto.Id != Guid.Empty && await repository.Get(produto.Id) != null)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Já existe um produto com esse ID",
                    Data = null
                };

            produto.GeraID();
            await repository.Save(produto);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto cadastrado com sucesso!",
                Data = produto
            };
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<ResultViewModel>> Put([FromServices] IProdutoRepository repository, [FromServices] IMapper mapper, [FromBody] ProdutoModel model)
        {

            var produto = mapper.Map<Produto>(model);
            produto.Validate();

            if (produto.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar o produto",
                    Data = produto.Notifications
                };

            produto = await repository.Get(model.Id);
            if (produto == null)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Produto não encontrado",
                    Data = null
                };

            produto.AlterarNome(model.Nome);
            produto.AlterarValor(model.Valor);
            produto.AlterarImagem(model.Imagem);

            await repository.Update(produto);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto alterado com sucesso!",
                Data = produto
            };
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<ResultViewModel>> Delete([FromServices] IProdutoRepository repository, [FromBody] ProdutoModel model)
        {
            var produto = await repository.Get(model.Id);

            if (produto == null)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Produto não encontrado",
                    Data = null
                };

            await repository.Delete(produto);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto excluído com sucesso!",
                Data = null
            };
        }
    }
}