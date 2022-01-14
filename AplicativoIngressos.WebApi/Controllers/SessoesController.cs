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


    }
}
