using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiteMercadoAPI.Application.Models;
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
            //return await repository.Get();
            //return mapper.Map<List<ProdutoModel>>(await repository.Get());
            return new List<ProdutoModel>();
        }

        // [HttpGet]
        // [Route("{id:int}")]
        // public async Task<ActionResult<Product>> Get([FromServices] IProductRepository repository, int id)
        // {
        //     return await repository.Get(id);
        // }

        // [HttpPost]
        // [Route("")]
        // public async Task<ActionResult<ResultViewModel>> Post([FromServices] IProductRepository repository, [FromBody] EditorProductViewModel model)
        // {

        //     model.Validate();
        //     if (model.Invalid)
        //         return new ResultViewModel
        //         {
        //             Success = false,
        //             Message = "Não foi possível inserir o produto",
        //             Data = model.Notifications
        //         };

        //     var product = new Product();
        //     product.Title = model.Title;
        //     product.CategoryId = model.CategoryId;
        //     product.CreateDate = DateTime.Now;
        //     product.Description = model.Description;
        //     product.Image = model.Image;
        //     product.LastUpdateDate = DateTime.Now;
        //     product.Price = model.Price;
        //     product.Quantity = model.Quantity;

        //     await repository.Save(product);

        //     return new ResultViewModel
        //     {
        //         Success = true,
        //         Message = "Produto cadastrado com sucesso!",
        //         Data = product
        //     };
        // }

        // [HttpPut]
        // [Route("")]
        // public async Task<ActionResult<ResultViewModel>> Put([FromServices] IProductRepository repository, [FromBody] EditorProductViewModel model)
        // {

        //     model.Validate();
        //     if (model.Invalid)
        //         return new ResultViewModel
        //         {
        //             Success = false,
        //             Message = "Não foi possível alterar o produto",
        //             Data = model.Notifications
        //         };

        //     var product = await repository.Get(model.Id);
        //     product.Title = model.Title;
        //     product.CategoryId = model.CategoryId;
        //     //product.CreateDate = DateTime.Now;
        //     product.Description = model.Description;
        //     product.Image = model.Image;
        //     product.LastUpdateDate = DateTime.Now;
        //     product.Price = model.Price;
        //     product.Quantity = model.Quantity;

        //     await repository.Update(product);

        //     return new ResultViewModel
        //     {
        //         Success = true,
        //         Message = "Produto alterado com sucesso!",
        //         Data = product
        //     };
        // }

        // [HttpDelete]
        // [Route("")]
        // public async Task<ActionResult<Product>> Delete([FromServices] IProductRepository repository, [FromBody] Product model)
        // {
        //     await repository.Delete(model);
        //     return model;
        // }
    }
}