using LH_PET.Context;
using LH_PET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LH_PET.Controllers
{
    public class DadosAnimalController : Controller
    {
        private readonly AppDbContext _context;

        public DadosAnimalController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult BuscarAnimal(string busca)
        {
            var resultado = _context.Animais.ToList();
            if (!string.IsNullOrEmpty(busca))
            {
                busca = busca.ToLower();

                resultado = _context.Animais.Where(c => c.Nome.ToLower().Contains(busca) ||
                c.Tipo.ToLower().Contains(busca) ||
                c.Raca.ToLower().Contains(busca) ||
                c.Idade.ToLower().Contains(busca)).ToList();


            }
            return View(resultado);

        }

        [HttpGet]
        public JsonResult BuscarClientes(string termo)
        {
            var nomes = _context.Clientes
                .Where(c => c.Nome.ToLower().Contains(termo.ToLower()))
                .Select(c => new { id = c.ClienteID, nome = c.Nome })
                .ToList();

            return Json(nomes);
        }


        [HttpGet]
        public IActionResult CreateAnimal()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateAnimal(Animal animais)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var erros = ModelState.Values.SelectMany(v => v.Errors)
                                                 .Select(e => e.ErrorMessage)
                                                 .ToList();
                    TempData["Erros"] = string.Join(" | ", erros);
                    return View(animais);
                }


                _context.Animais.Add(animais);
                _context.SaveChanges();

                return RedirectToAction("BuscarAnimal");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao criar animal: " + ex.Message);
                return View(animais);
            }
        }


        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            var cliente = _context.Clientes
                .Include(c => c.Animais) // Caso queira mostrar os animais também
                .FirstOrDefault(c => c.ClienteID == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpGet]
        public IActionResult EditarAnimal(int id)
        {
            var animal = _context.Animais.Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        [HttpPost]
        public IActionResult EditarAnimal(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return View(animal);
            }

            _context.Animais.Update(animal);
            _context.SaveChanges();

            return RedirectToAction("Detalhes", "DadosAnimal", new { id = animal.ClienteID });
        }

        [HttpGet]
        public IActionResult ExcluirAnimal(int id)
        {
            var animal = _context.Animais.Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            int clienteId = animal.ClienteID;

            _context.Animais.Remove(animal);
            _context.SaveChanges();

            return RedirectToAction("Detalhes", new { id = clienteId });
        }


    }
}
