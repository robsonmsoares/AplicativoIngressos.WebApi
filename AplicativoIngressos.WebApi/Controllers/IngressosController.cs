using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoIngressos.WebApi.Infraestrutura;
using AplicacaoIngressos.WebApi.Dominio;
using AplicacaoIngressos.WebApi.Models;
using Microsoft.Extensions.Logging;

namespace AplicacaoIngressos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngressosController : ControllerBase
    {
        private readonly IngressosRepositorio _ingressosRepositorio;
        private readonly SessoesRepositorio _sessoesRepositorio;
        private readonly ILogger<IngressosController> _logger;

        public IngressosController(ILogger<IngressosController> logger, IngressosRepositorio ingressosRepositorio, SessoesRepositorio sessoesRepositorio)
        {
            _logger = logger;
            _ingressosRepositorio = ingressosRepositorio;
            _sessoesRepositorio = sessoesRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarTodos(CancellationToken cancellationToken)
        {
            var ingressos = await _ingressosRepositorio.RecuperarTodos(cancellationToken);

            return Ok(ingressos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorId(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("ID não pode ser convertida");

            var ingresso = await _ingressosRepositorio.RecuperarPorId(guid, cancellationToken);

            return Ok(ingresso);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] NovoIngressoInputModel novoIngressoInputModel, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(novoIngressoInputModel.SessaoId, out var guid))
                return BadRequest("ID da sessão não pôde ser convertido");

            var sessao = await _sessoesRepositorio.RecuperarPorId(guid, cancellationToken);

            if (sessao == null)
                return NotFound();

            var ingressoSessao = await _sessoesRepositorio.RecuperarPorId(guid, cancellationToken);

            var ingressosVendidos = ingressoSessao.Ingressos.Select(x => x.Quantidade).Sum();

            if (ingressosVendidos == ingressoSessao.QuantidadeLugares)
                return BadRequest("Sessão já está lotada");

            var ingressosRestantes = ingressoSessao.QuantidadeLugares - ingressosVendidos;

            if (ingressosRestantes < novoIngressoInputModel.Quantidade)
                return BadRequest($"Não há ingressos suficientes a venda. Restam apenas {ingressosRestantes} ingressos!");

            var novoIngresso = Ingresso.Criar(Guid.Parse(novoIngressoInputModel.SessaoId), novoIngressoInputModel.NomeCliente, novoIngressoInputModel.Quantidade);
            if (novoIngresso.IsFailure)
            {
                _logger.LogInformation($"Erro: {novoIngresso.Error}");
                return BadRequest(novoIngresso.Error);
            }

            await _ingressosRepositorio.Inserir(novoIngresso.Value, cancellationToken);
            await _ingressosRepositorio.Commit(cancellationToken);

            _logger.LogInformation($"Ingresso {novoIngresso.Value.Id} vendido com sucesso");

            return CreatedAtAction("RecuperarPorId", new { id = novoIngresso.Value.Id }, novoIngresso.Value.Id);
        }
    }
}