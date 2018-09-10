using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Context;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Biblioteca.Controllers
{
    [Route("[controller]/[action]")]
    public class EmprestimoController : Controller
    {
        private readonly BibliotecaContexto bibliotecaContexto;

        public EmprestimoController(BibliotecaContexto bibliotecaContexto)
        {
            this.bibliotecaContexto = bibliotecaContexto;
        }

        public IActionResult Detalhar(string id)
        {
            Guid gId = Guid.Parse(id);

            Emprestimo emprestimo = (Emprestimo)bibliotecaContexto.Emprestimos
                                                                .Include(x => x.Cliente)
                                                                .Include(x => x.Livro)
                                                                .ToList()
                                                                .FirstOrDefault(x => x.Id == gId);

            return View("./Views/Emprestimo/Detalhar.cshtml", emprestimo);
        }

        public IActionResult Excluir(string id)
        {
            Guid gId = Guid.Parse(id);

            Emprestimo emprestimo = (Emprestimo)bibliotecaContexto.Emprestimos.ToList().FirstOrDefault(x => x.Id == gId);

            bibliotecaContexto.Emprestimos.Remove(emprestimo);

            bibliotecaContexto.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var emprestimos = bibliotecaContexto.Emprestimos
                             .Include(x => x.Cliente)
                             .Include(x => x.Livro).ToList();

            return View("./Views/Emprestimo/Emprestimos.cshtml" , emprestimos);
        }

        public IActionResult Editar(string id)
        {
            Guid gId = Guid.Parse(id);
            
            Emprestimo emprestimo = (Emprestimo)bibliotecaContexto.Emprestimos.ToList().FirstOrDefault(x => x.Id == gId);

            ViewBag.NomesDosClientes = this.bibliotecaContexto.Clientes.ToList().Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();

            ViewBag.TitulosDosLivros = this.bibliotecaContexto.Livros.ToList().Select(c => new SelectListItem()
            { Text = c.Titulo, Value = c.Id.ToString() }).ToList();

            return View("./Views/Emprestimo/Editar.cshtml", emprestimo);
        }

        public IActionResult Novo()
        {
            ViewBag.NomesDosClientes = this.bibliotecaContexto.Clientes.ToList().Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();

            ViewBag.TitulosDosLivros = this.bibliotecaContexto.Livros.ToList().Where(x => x.Situacao == 'A').Select(c => new SelectListItem()
            { Text = c.Titulo, Value = c.Id.ToString() }).ToList();

            return View("./Views/Emprestimo/Manter.cshtml");
        }
        
        [HttpPost]
        public IActionResult Salvar(Emprestimo model)
        {
            if (model.Id.Equals(new Guid()))
            {
                model.Id = Guid.NewGuid();
                Livro livro = (Livro)bibliotecaContexto.Livros.ToList().FirstOrDefault(x => x.Id == model.IdLivro);

                livro.Situacao = 'I';
                bibliotecaContexto.Livros.Update(livro);

                bibliotecaContexto.Emprestimos.Add(model);
            }
            else
            {
                bibliotecaContexto.Emprestimos.Update(model);
            }

            bibliotecaContexto.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}