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

            ViewBag.TitulosDosLivros = this.bibliotecaContexto.Livros.ToList().Select(c => new SelectListItem()
            { Text = c.Titulo, Value = c.Id.ToString() }).ToList();

            return View("./Views/Emprestimo/Manter.cshtml");
        }
        
        [HttpPost]
        public IActionResult Salvar(Emprestimo model)
        {
            if (model.Id.Equals(new Guid()))
            {
                model.Id = Guid.NewGuid();

                bibliotecaContexto.Emprestimos.Add(model);
            }
            else
            {
                bibliotecaContexto.Emprestimos.Update(model);
            }

            bibliotecaContexto.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var emprestimos = bibliotecaContexto.Emprestimos
               .Include(x => x.Cliente)
               .Include(x => x.Livro)
               .ToList();

            if (emprestimos == null) return NoContent();

            return Ok(emprestimos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var emprestimo = bibliotecaContexto.Emprestimos.FirstOrDefault(x => x.Id == id);

            if (emprestimo == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(emprestimo);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Emprestimo emprestimo)
        {
            emprestimo.Id = id;

            bibliotecaContexto.Emprestimos.Update(emprestimo);

            bibliotecaContexto.SaveChanges();

            return Ok(bibliotecaContexto.Emprestimos.ToList());

        }

        [HttpPost]
        public IActionResult Post([FromBody] Emprestimo emprestimo)
        {
            emprestimo.Id = Guid.NewGuid();
            bibliotecaContexto.Emprestimos.Add(emprestimo);
            bibliotecaContexto.SaveChanges();

            return Ok(bibliotecaContexto.Emprestimos.ToList());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Emprestimo emprestimo = bibliotecaContexto.Emprestimos.FirstOrDefault(x => x.Id == id);
            bibliotecaContexto.Emprestimos.Remove(emprestimo);

            bibliotecaContexto.SaveChanges();

            return Ok(bibliotecaContexto.Emprestimos.ToList());
        }
    }
}