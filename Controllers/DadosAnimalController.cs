using LH_PET.Context;
using LH_PET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LH_PET.Controllers
{
    [Authorize]
    public class DadosAnimalController : Controller
    {
        private readonly AppDbContext _context;

        public DadosAnimalController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> BuscarAnimalAsync(string busca)
        {
            var resultado = await _context.Animais.ToListAsync();
            if (!string.IsNullOrEmpty(busca))
            {
                busca = busca.ToLower();

                resultado = await _context.Animais.Where(c => c.Nome.ToLower().Contains(busca) ||
                c.Tipo.ToLower().Contains(busca) ||
                c.Raca.ToLower().Contains(busca) ||
                c.Idade.ToLower().Contains(busca)).ToListAsync();


            }
            return View(resultado);

        }

        [HttpGet]
        public async Task<JsonResult> BuscarClientesAsync(string termo)
        {
            var nomes = await _context.Clientes
                .Where(c => c.Nome.ToLower().Contains(termo.ToLower()))
                .Select(c => new { id = c.ClienteID, nome = c.Nome })
                .ToListAsync();

            return Json(nomes);
        }


        [HttpGet]
        public IActionResult CreateAnimal()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateAnimalAsync(Animal animais)
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


                await _context.Animais.AddAsync(animais);
                await _context.SaveChangesAsync();

                return RedirectToAction("BuscarAnimal");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao criar animal: " + ex.Message);
                return View(animais);
            }
        }


        [HttpGet]
        public async Task<ActionResult> DetalhesAsync(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Animais) 
                .FirstOrDefaultAsync(c => c.ClienteID == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpGet]
        public async Task<ActionResult> EditarAnimalAsync(int id)
        {
            var animal = await _context.Animais.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        [HttpPost]
        public async Task<ActionResult> EditarAnimalAsync(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return View(animal);
            }

            _context.Animais.Update(animal);
            await _context.SaveChangesAsync();

            return RedirectToAction("Detalhes", "DadosAnimal", new { id = animal.ClienteID });
        }

        [HttpGet]
        public async Task<ActionResult> ExcluirAnimalAsync(int id)
        {
            var animal = await _context.Animais.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            int clienteId = animal.ClienteID;

            _context.Animais.Remove(animal);
            await _context.SaveChangesAsync();

            return RedirectToAction("Detalhes", new { id = clienteId });
        }


    }
}
