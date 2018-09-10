using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using Biblioteca.Context;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly BibliotecaContexto _bibliotecaContexto;

        public HomeController(BibliotecaContexto bibliotecaContexto)
        {
            _bibliotecaContexto = bibliotecaContexto;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Livros()
        {
            return View("./Views/Livro/Livros.cshtml", _bibliotecaContexto.Livros.ToList());
        }

        public IActionResult Emprestimos()
        {
            return View(_bibliotecaContexto.Emprestimos.ToList());
        }

        public IActionResult Clientes()
        {
            return View(_bibliotecaContexto.Clientes.ToList());
        }

    }
}
