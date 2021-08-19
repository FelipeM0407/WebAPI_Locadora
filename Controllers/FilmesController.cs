using Locadora_CineClub.Data;
using Locadora_CineClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora_CineClub.Controllers
{
    [ApiController]
    [Route("v1/filmes")]
    public class FilmesController : ControllerBase
    {
        [HttpGet]
        [Route("selecionar/{id:int?}")]
        public async Task<ActionResult<List<Filme>>> Selecionar([FromServices] DataContext context, int id)
        {
            var filmes = await context.Filmes.ToListAsync();
            if (id > 0)
                return filmes.FindAll(a => a.Id == id);
            else
                return filmes;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<ActionResult<Filme>> Cadastrar([FromServices] DataContext context, [FromBody] Filme filme)
        {
            if (ModelState.IsValid)
            {
                var obj_filme = await context.Filmes.FirstOrDefaultAsync(a => a.Nome == filme.Nome.Trim());

                if (obj_filme == null)
                {
                    filme.Nome = filme.Nome.Trim();
                    filme.Ativo = true;
                    filme.Disponivel = true;
                    filme.Dt_Cadastro = DateTime.Now;
                    context.Filmes.Add(filme);
                    await context.SaveChangesAsync();
                    return filme;
                }
                else
                {
                    return Content("Filme já Cadastrado !");
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("inativar/{id:int?}")]
        public async Task<ActionResult<Filme>> Inativar([FromServices] DataContext context, int id)
        {
            if (id != 0)
            {
                var filme = await context.Filmes.FirstOrDefaultAsync(c => c.Id == id);

                if (filme == null)
                {
                    return Content("Filme não Encontrado !");
                }

                filme.Ativo = false;
                context.Filmes.Update(filme);
                await context.SaveChangesAsync();
                return filme;
            }
            else
            {
                return Content("Informe o Id do Filme !");
            }
        }

        [HttpPost]
        [Route("ativar/{id:int?}")]
        public async Task<ActionResult<Filme>> Ativar([FromServices] DataContext context, int id)
        {
            if (id != 0)
            {
                var filme = await context.Filmes.FirstOrDefaultAsync(c => c.Id == id);

                if (filme == null)
                {
                    return Content("Filme não Encontrado !");
                }

                filme.Ativo = true;
                context.Filmes.Update(filme);
                await context.SaveChangesAsync();
                return filme;
            }
            else
            {
                return Content("Informe o Id do Filme !");
            }
        }
    }
}
