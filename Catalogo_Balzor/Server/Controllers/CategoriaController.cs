using Catalogo_Balzor.Server.Context;
using Catalogo_Balzor.Server.Utils;
using Catalogo_Balzor.Shared.Models;
using Catalogo_Balzor.Shared.Recursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Balzor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get([FromQuery] Paginacao paginacao,
                                                             [FromQuery] string Nome,
                                                             [FromServices]AppDbContext Context)
        {
            var queryable = Context.Categorias.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Nome))
            {
                queryable = queryable.Where(px => px.Nome.Contains(Nome));
            }

            await HttpContext.InserirParametroEmPageResponse(queryable, paginacao.QuantidadePorPagina);

            return await queryable.Paginar(paginacao).ToListAsync();
            //return await Context.Categorias.AsNoTracking().ToListAsync();
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] AppDbContext Context)
        {
            return await Context.Categorias.AsNoTracking().ToListAsync();
        }


        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id, [FromServices]AppDbContext Context)
        {
            return await Context.Categorias.AsNoTracking().FirstOrDefaultAsync(px => px.CategoriaId == id);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria,[FromServices] AppDbContext Context)
        {
            Context.Categorias.Add(categoria);
            await Context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut]
        public async Task<ActionResult<Categoria>> Put(Categoria categoria, [FromServices] AppDbContext Context)
        {
            Context.Entry(categoria).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id, [FromServices] AppDbContext Context)
        {
            var categoria = Context.Categorias.Find(id);
            Context.Categorias.Remove(categoria);
            await Context.SaveChangesAsync();
            return Ok(categoria);
        }

    }
}
