using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoIngressos.WebApi.Infraestrutura;
using AplicacaoIngressos.WebApi.Dominio;
using AplicacaoIngressos.WebApi.Models;

namespace AplicacaoIngressos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngressosController : ControllerBase
    {
        private readonly IngressosRepositorio _ingressosRepositorio;

        public IngressosController(IngressosRepositorio ingressosRepositorio)
        {
            _ingressosRepositorio = ingressosRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAsync([FromBody] NovoIngressoInputModel ingressoInputModel, CancellationToken cancellationToken)
        {
            var ingresso = Ingresso.Criar(Guid.Parse(ingressoInputModel.SessaoId), ingressoInputModel.NomeCliente, ingressoInputModel.Quantidade);
            if (ingresso.IsFailure)
                return BadRequest(ingresso.Error);
            await _ingressosRepositorio.InserirAsync(ingresso.Value, cancellationToken);
            await _ingressosRepositorio.CommitAsync(cancellationToken);
            return CreatedAtAction("RecuperarPorId", new { id = ingresso.Value.Id }, ingresso.Value.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorId(Guid id, CancellationToken cancellationToken)
        {
            var ingresso = await _ingressosRepositorio.RecuperarPorIdAsync(id, cancellationToken);

            return Ok(ingresso);
        }
    }
}
