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

        [HttpGet]
        public IActionResult GetAll()
        {
            var clientes = bibliotecaContexto.Clientes.ToList();
            
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var cliente = bibliotecaContexto.Clientes.FirstOrDefault(x => x.Id == id);

            if (cliente == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(cliente);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Cliente cliente)
        {
                cliente.Id = id;

                bibliotecaContexto.Clientes.Update(cliente);

                bibliotecaContexto.SaveChanges();

                return Ok(bibliotecaContexto.Clientes.ToList());
         
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            cliente.Id = Guid.NewGuid();
            bibliotecaContexto.Clientes.Add(cliente);
            bibliotecaContexto.SaveChanges();

            return Ok(bibliotecaContexto.Clientes.ToList());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Cliente cliente =bibliotecaContexto.Clientes.FirstOrDefault(x => x.Id == id);
            bibliotecaContexto.Clientes.Remove(cliente);

            bibliotecaContexto.SaveChanges();

            return Ok(bibliotecaContexto.Clientes.ToList());
        }

    }
}