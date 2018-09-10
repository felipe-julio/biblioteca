using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroApiController : Controller
    {
        private readonly BibliotecaContexto bibliotecaContexto;

        public LivroApiController(BibliotecaContexto bibliotecaContexto)
        {
            this.bibliotecaContexto = bibliotecaContexto;
        }

        [HttpGet]
        [Route("livrosativos")]
        public IActionResult GetAll()
        {
            var clientes = bibliotecaContexto.Livros.ToList().Where(x => x.Situacao == 'A');

            return Ok(clientes);
        }
    }
}