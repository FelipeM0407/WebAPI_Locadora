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
    [Route("v1/clientes")]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        [Route("selecionar/{id:int?}")]
        public async Task<ActionResult<List<Cliente>>> Selecionar([FromServices] DataContext context, int id)
        {
            var clientes = await context.Clientes.ToListAsync();
            if (id > 0)
                return clientes.FindAll(a => a.Id == id);
            else
                return clientes;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<ActionResult<Cliente>> Cadastrar([FromServices] DataContext context, [FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var obj_cliente = await context.Clientes.FirstOrDefaultAsync(a => a.Nome == cliente.Nome.Trim());

                if (obj_cliente == null)
                {
                    cliente.Nome = cliente.Nome.Trim();
                    cliente.Ativo = true;
                    cliente.Dt_Cadastro = DateTime.Now;
                    context.Clientes.Add(cliente);
                    await context.SaveChangesAsync();
                    return cliente;
                }
                else
                {
                    return Content("Cliente já Cadastrado !");
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("inativar/{id:int?}")]
        public async Task<ActionResult<Cliente>> Inativar([FromServices] DataContext context, int id)
        {
            if (id != 0)
            {
                var cliente = await context.Clientes.FirstOrDefaultAsync(c => c.Id == id);

                if (cliente == null)
                {
                    return Content("Cliente não Encontrado !");
                }

                cliente.Ativo = false;
                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();
                return cliente;
            }
            else
            {
                return Content("Informe o Id do Cliente !");
            }
        }

        [HttpPost]
        [Route("ativar/{id:int?}")]
        public async Task<ActionResult<Cliente>> Ativar([FromServices] DataContext context, int id)
        {
            if (id != 0)
            {
                var cliente = await context.Clientes.FirstOrDefaultAsync(c => c.Id == id);

                if (cliente == null)
                {
                    return Content("Cliente não Encontrado !");
                }

                cliente.Ativo = true;
                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();
                return cliente;
            }
            else
            {
                return Content("Informe o Id do Cliente !");
            }
        }
    }
}
