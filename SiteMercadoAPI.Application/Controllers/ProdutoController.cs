using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<ResultViewModel>> Post([FromServices] IProdutoRepository repository, [FromServices] IMapper mapper, [FromForm] ProdutoModel model)
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

            var fileID = Guid.NewGuid() + "_";

            if (!await UploadFile(model.ImagemArquivo, fileID))
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Ocorreu um problema ao salvar a imagem",
                    Data = null
                };
            }

            produto.AlterarImagem(fileID + model.ImagemArquivo.FileName);
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
        public async Task<ActionResult<ResultViewModel>> Put([FromServices] IProdutoRepository repository, [FromServices] IMapper mapper, [FromForm] ProdutoModel model)
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

            if (model.ImagemArquivo != null)
            {
                var fileID = Guid.NewGuid() + "_";
                if (!await UploadFile(model.ImagemArquivo, fileID))
                {
                    return new ResultViewModel
                    {
                        Success = false,
                        Message = "Ocorreu um problema ao salvar a imagem",
                        Data = null
                    };
                }
                produto.AlterarImagem(fileID + model.ImagemArquivo.FileName);
            }

            produto.AlterarNome(model.Nome);
            produto.AlterarValor(model.Valor);

            await repository.Update(produto);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto alterado com sucesso!",
                Data = produto
            };
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult<ResultViewModel>> Delete([FromServices] IProdutoRepository repository, Guid id)
        {
            var produto = await repository.Get(id);

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
        private async Task<bool> UploadFile(IFormFile file, string fileID)
        {
            if (file.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileID + file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

        private bool CheckImageExists(string path)
        {

            if (System.IO.File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}