using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Context;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Biblioteca.Controllers
{
    [Route("[controller]/[action]")]
    public class LivroController : Controller
    {
        private readonly BibliotecaContexto bibliotecaContexto;

        public LivroController(BibliotecaContexto bibliotecaContexto)
        {
            this.bibliotecaContexto = bibliotecaContexto;
        }

        public IActionResult Index()
        {
            return View("./Views/Livro/Livros.cshtml", bibliotecaContexto.Livros.ToList());
        }

        public IActionResult detalhar(string id)
        {
            Guid gId = Guid.Parse(id);
 
            Livro livro = (Livro) bibliotecaContexto.Livros.ToList().FirstOrDefault(x => x.Id == gId);

            return View("./Views/Livro/Detalhar.cshtml", livro);
        }

        public IActionResult Editar(string id)
        {
            Guid gId = Guid.Parse(id);

            Livro livro = (Livro)bibliotecaContexto.Livros.ToList().FirstOrDefault(x => x.Id == gId);

            return View("./Views/Livro/Editar.cshtml", livro);
        }

        public IActionResult Novo()
        {
            return View("./Views/Livro/Manter.cshtml");
        }

        [HttpPost]
        public ActionResult Salvar(Livro model)
        {
            if (model.Id.Equals(new Guid()))
            {
                model.Id = Guid.NewGuid();
                model.Situacao = 'A';

                bibliotecaContexto.Livros.Add(model);

            } else
            {
                IEnumerable<string> items = new List<string>()
                {
                    "Ativo",
                    "Inativo"
                };

                ViewBag.Situacao = items;
                
                bibliotecaContexto.Livros.Update(model);
            }

            bibliotecaContexto.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Excluir(string id)
        {
            Guid gId = Guid.Parse(id);

            Livro livro = bibliotecaContexto.Livros.ToList().FirstOrDefault(x => x.Id == gId);

            bibliotecaContexto.Livros.Remove(livro);

            bibliotecaContexto.SaveChanges();

            return RedirectToAction("Index");
        }
     
    }
}