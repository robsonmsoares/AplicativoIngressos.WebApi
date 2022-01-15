using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoIngressos.WebApi.Infraestrutura;
using AplicacaoIngressos.WebApi.Dominio;
using AplicacaoIngressos.WebApi.Models;

namespace AplicacaoIngressos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly FilmesRepositorio _filmesRepositorio;

        public FilmesController(FilmesRepositorio filmesRepositorio)
        {
            _filmesRepositorio = filmesRepositorio;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorId(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Id inválido");
            var filme = await _filmesRepositorio.RecuperarPorId(guid, cancellationToken);
            if (filme == null)
                return NotFound();
            return Ok(filme);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarTodos(CancellationToken cancellationToken)
        {
            var filmes = await _filmesRepositorio.RecuperarTodos(cancellationToken);

            return Ok(filmes);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] NovoFilmeInputModel novoFilmeInputModel, CancellationToken cancellationToken)
        {
            var novoFilme = Filme.Criar(novoFilmeInputModel);
            if (novoFilme.IsFailure)
                return BadRequest(novoFilme.Error);

            await _filmesRepositorio.Inserir(novoFilme.Value, cancellationToken);
            await _filmesRepositorio.Commit(cancellationToken);

            return CreatedAtAction("RecuperarPorId", new { id = novoFilme.Value.Id }, novoFilme.Value.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, [FromBody] AtualizarFilmeInputModel atualizarFilmeInputModel, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("ID não pode ser convertida");

            var filme = await _filmesRepositorio.RecuperarPorId(guid, cancellationToken);

            if (filme == null)
                return NotFound();

            filme.Atualizar(atualizarFilmeInputModel);
            _filmesRepositorio.Atualizar(filme);
            await _filmesRepositorio.Commit(cancellationToken);

            return Ok(filme);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("ID não pode ser convertida");

            var filme = await _filmesRepositorio.RecuperarPorId(guid, cancellationToken);

            if (filme == null)
                return NotFound();

            _filmesRepositorio.Remover(filme);
            await _filmesRepositorio.Commit(cancellationToken);

            return Ok("Filme foi removido com sucesso!");
        }

    }
}
