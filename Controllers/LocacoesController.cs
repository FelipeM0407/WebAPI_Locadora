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
    [Route("v1/locacoes")]
    public class LocacoesController : ControllerBase
    {
        [HttpPost]
        [Route("alugar")]
        public async Task<ActionResult<Locacao>> Alugar([FromServices] DataContext context, [FromBody] Locacao locacao)
        {
            if (locacao.ClienteId != 0 && locacao.FilmeId != 0 && locacao.Dt_Entrega != DateTime.MinValue)
            {
                var filme = context.Filmes.FirstOrDefault(a => a.Id == locacao.FilmeId);
                var cliente = context.Clientes.FirstOrDefault(a => a.Id == locacao.ClienteId);

                if (filme == null)
                {
                    return Content("Filme não Encontrado !");

                }
                else if (!filme.Disponivel)
                {
                    return Content("'" + filme.Nome + "' não esta Disponível !");
                }
                else if (!filme.Ativo)
                {
                    return Content("Filme '" + filme.Nome + "' Inativo !");
                }
                else if (cliente == null)
                {
                    return Content("Cliente não Encontrado !");
                }
                else if (!cliente.Ativo)
                {
                    return Content("Cliente '" + cliente.Nome + "' Inativo !");
                }
                else
                {
                    filme.Disponivel = false;
                    context.Filmes.Update(filme); //Indisponibiliza o Filme

                    locacao.Dt_Alocacao = DateTime.Now;
                    context.Locacoes.Add(locacao);

                    await context.SaveChangesAsync();
                    return Content("'" + filme.Nome + "' alugado com Sucesso !");
                }

            }
            else
            {
                return Content("Informe o Id do Cliente, do Filme e a Data da Entrega yyyy-MM-dd!");
            }
        }

        [HttpPost]
        [Route("devolver")]
        public async Task<ActionResult<Locacao>> Devolver([FromServices] DataContext context, Locacao locacao)
        {
            if (locacao.ClienteId != 0 && locacao.FilmeId != 0)
            {
                var cliente = context.Clientes.FirstOrDefault(a => a.Id == locacao.ClienteId);
                var filme = context.Filmes.FirstOrDefault(a => a.Id == locacao.FilmeId);

                if (cliente == null)
                {
                    return Content("Cliente não Encontrado !");
                }
                else if (filme == null)
                {
                    return Content("Filme não Encontrado !");
                }
                else if (filme.Disponivel)
                {
                    return Content("'" + filme.Nome + "' esta disponível e/ou ja foi devolvido !");
                }

                var obj_locacao = context.Locacoes.FirstOrDefault(a => a.ClienteId == locacao.ClienteId && a.FilmeId == locacao.FilmeId && a.Dt_Devolvido == DateTime.MinValue);

                if (obj_locacao == null)
                    return Content("O Filme não foi alugado por esse cliente !");


                filme.Disponivel = true;
                locacao.Dt_Devolvido = DateTime.Now;

                context.Filmes.Update(filme);
                context.Locacoes.Update(locacao);
                await context.SaveChangesAsync();

                if (DateTime.Now > obj_locacao.Dt_Entrega)
                {
                    return Content("Filme Devolvido apos a Entrega Prevista, cobrar as devidas Taxas !");
                }
                else
                {
                    return Content("Filme Devolvido com Sucesso !");
                }

            }
            else
            {
                return Content("Informe o Id do Cliente e do Filme");
            }
        }
    }
}
