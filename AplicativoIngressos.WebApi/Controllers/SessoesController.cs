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
    public class SessoesController : ControllerBase
    {
        private readonly SessoesRepositorio _sessoesRepositorio;
        private readonly FilmesRepositorio _filmesRepositorio;

        public SessoesController(SessoesRepositorio sessoesRepositorio, FilmesRepositorio filmesRepositorio)
        {
            _sessoesRepositorio = sessoesRepositorio;
            _filmesRepositorio = filmesRepositorio;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorId(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("ID não pôde ser convertido");

            var sessao = await _sessoesRepositorio.RecuperarPorId(guid, cancellationToken);

            return Ok(sessao);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarTodas([FromQuery] string filme, [FromQuery] string dataHora, CancellationToken cancellationToken)
        {
            var sessoes = await _sessoesRepositorio.RecuperarTodas(cancellationToken);

            if (!string.IsNullOrEmpty(filme))
            {
                if (!Guid.TryParse(filme, out var filmeGuid))
                    return BadRequest("ID do filme não pôde ser convertida");

                sessoes = sessoes.Where(sessao => sessao.FilmeId == filmeGuid);
            }

            if (!string.IsNullOrEmpty(dataHora))
            {
                if (!DateTime.TryParse(dataHora, out var datetime))
                    return BadRequest();

                sessoes = sessoes.Where(sessao => sessao.DataHora == datetime);
            }

            return Ok(sessoes);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] NovaSessaoInputModel novaSessaoInputModel, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(novaSessaoInputModel.FilmeId, out var guid))
                return BadRequest("ID do filme não pôde ser convertida");

            var sessao = Sessao.Criar(Guid.Parse(novaSessaoInputModel.FilmeId), DateTime.Parse(novaSessaoInputModel.DataHora),
                novaSessaoInputModel.QuantidadeLugares, novaSessaoInputModel.Preco);

            if (sessao.IsFailure)
                return BadRequest(sessao.Error);

            await _sessoesRepositorio.Inserir(sessao.Value, cancellationToken);
            await _sessoesRepositorio.Commit(cancellationToken);

            return CreatedAtAction("RecuperarPorId", new { id = sessao.Value.Id }, sessao.Value.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, [FromBody] AtualizarSessaoInputModel atualizarSessaoInputModel, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("ID não pôde ser convertido");

            var sessao = await _sessoesRepositorio.RecuperarPorId(guid, cancellationToken);

            if (sessao == null)
                return NotFound();

            if (!Guid.TryParse(atualizarSessaoInputModel.FilmeId, out var filmeId))
                return BadRequest("ID não pôde ser convertido");

            var filme = await _filmesRepositorio.RecuperarPorId(filmeId, cancellationToken);
            if (filme == null)
                return NotFound();

            sessao.Atualizar(atualizarSessaoInputModel);
            _sessoesRepositorio.Atualizar(sessao);
            await _sessoesRepositorio.Commit(cancellationToken);

            return Ok(sessao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("ID não pôde ser convertido");

            var sessao = await _sessoesRepositorio.RecuperarPorId(guid, cancellationToken);

            if (sessao == null)
                return NotFound();

            _sessoesRepositorio.Remover(sessao);
            await _sessoesRepositorio.Commit(cancellationToken);

            return Ok("Sessão foi removida com sucesso!");
        }
    }
}