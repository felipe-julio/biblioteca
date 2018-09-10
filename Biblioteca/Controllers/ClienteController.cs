using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Context;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [Route("[controller]/[action]")]
    public class ClienteController : Controller
    {
        private readonly BibliotecaContexto bibliotecaContexto;

        public ClienteController(BibliotecaContexto bibliotecaContexto)
        {
            this.bibliotecaContexto = bibliotecaContexto;
        }

        public IActionResult detalhar(string id)
        {
            Guid gId = Guid.Parse(id);

            Cliente cliente = (Cliente)bibliotecaContexto.Clientes.ToList().FirstOrDefault(x => x.Id == gId);

            return View("./Views/Cliente/Detalhar.cshtml", cliente);
        }

        public IActionResult Index()
        {
            return View("./Views/Cliente/Clientes.cshtml", bibliotecaContexto.Clientes.ToList());
        }

        public IActionResult Novo()
        {
            return View("./Views/Cliente/Manter.cshtml");
        }

        public IActionResult Editar(string id)
        {
            Guid gId = Guid.Parse(id);

            Cliente cliente = (Cliente)bibliotecaContexto.Clientes.ToList().FirstOrDefault(x => x.Id == gId);

            return View("./Views/Cliente/Editar.cshtml", cliente);
        }

        public ActionResult Excluir(string id)
        {
            Guid gId = Guid.Parse(id);

            Cliente cliente = (Cliente)bibliotecaContexto.Clientes.ToList().FirstOrDefault(x => x.Id == gId);

            bibliotecaContexto.Clientes.Remove(cliente);

            bibliotecaContexto.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Salvar(Cliente model)
        {
            if (model.Id.Equals(new Guid()))
            {
                model.Id = Guid.NewGuid();
                bibliotecaContexto.Clientes.Add(model);
            }
            else
            {
                bibliotecaContexto.Clientes.Update(model);
            }

            bibliotecaContexto.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}