using Catalogo_Balzor.Server.Context;
using Catalogo_Balzor.Server.Utils;
using Catalogo_Balzor.Shared.Models;
using Catalogo_Balzor.Shared.Recursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Balzor.Server.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        [HttpGet("todos")]
        public async Task<ActionResult<List<Produto>>> Get([FromServices] AppDbContext Context)
        {
            return await Context
                .Produtos
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get(
            [FromQuery] Paginacao paginacao,
            [FromQuery]string Nome,
            [FromServices] AppDbContext Context)
        {
            var queryable = Context.Produtos.AsQueryable();

            if(!string.IsNullOrWhiteSpace(Nome))
            {
                queryable = queryable.AsNoTracking().Where(px => px.Nome.Contains(Nome));
            }

            await HttpContext.InserirParametroEmPageResponse(queryable, paginacao.QuantidadePorPagina);

            return await queryable.Paginar(paginacao).ToListAsync();
        }

        [HttpGet("categorias/{id:int}")]
        public async Task<ActionResult<List<Produto>>> GetProdutosCategoria(
            int id,
            [FromServices] AppDbContext Context)
        {
            var list = await Context.Produtos.AsNoTracking().Where(px => px.CategoriaId == id).ToListAsync();
            return list;
        }


        [HttpGet("{id}", Name = "GetProduto")]
        public async Task<ActionResult<Produto>> Get(
            int id,
            [FromServices] AppDbContext Context)
        {
            return await Context
                .Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(px => px.ProdutoId == id);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post(
            Produto produto, 
            [FromServices]AppDbContext Context)
        {
            Context.Produtos.Add(produto);
            await Context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetProduto", 
                new { id = produto.ProdutoId }, 
                produto);
        }

        [HttpPut]
        public async Task<ActionResult<Produto>> Put(
            Produto produto,
            [FromServices] AppDbContext Context)
        {
            Context.Entry(produto).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> Delete(
            int id,
            [FromServices] AppDbContext Context)
        {
            var produtos = Context.Produtos.Find(id);
            Context.Produtos.Remove(produtos);
            await Context.SaveChangesAsync();
            return Ok(produtos);
        }


    }
}
