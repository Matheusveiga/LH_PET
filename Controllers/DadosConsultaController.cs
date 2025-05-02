using LH_PET.Context;
using LH_PET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LH_PET.Controllers
{
    [Authorize]
    public class DadosConsultaController : Controller
    {
        private readonly AppDbContext _context;

        public DadosConsultaController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Agendar()
        {

            var clientes = await _context.Clientes.ToListAsync() ?? new List<Cliente>();
            var animais = await _context.Animais.ToListAsync() ?? new List<Animal>();


            if (clientes.Count == 0) ModelState.AddModelError(string.Empty, "Não há clientes cadastrados.");
            if (animais.Count == 0) ModelState.AddModelError(string.Empty, "Não há animais cadastrados.");

            ViewBag.Clientes = new SelectList(clientes, "ClienteID", "Nome"); // Use o nome correto da propriedade (ClienteID)
            ViewBag.Animais = new SelectList(animais, "AnimalID", "Nome"); // Use o nome correto da propriedade (AnimalID)

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Agendar(Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Consultas.Add(consulta);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Consulta");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao salvar: {ex.Message}");
                }
            }

            return View(consulta);
        }
        // Listar todas as consultas agendadas
        [HttpGet]
        public async Task<ActionResult> Consulta()
        {
            // Incluir clientes e animais para mostrar na listagem
            var consultas = await _context.Consultas
                .Include(c => c.Cliente)
                .Include(c => c.Animal)
                .ToListAsync();

            return View(consultas);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarClientes(string term)
        {
            var clientes = await _context.Clientes
                .Where(c => c.Nome.Contains(term))
                .Select(c => new { clienteID = c.ClienteID, nome = c.Nome })
                .ToListAsync();

            return Json(clientes);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarAnimais(string term)
        {
            var animais = await _context.Animais
                .Where(a => a.Nome.Contains(term))
                .Select(a => new { animalID = a.AnimalID, nome = a.Nome })
                .ToListAsync();

            return Json(animais);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var consulta = await _context.Consultas
                .Include(c => c.Cliente)
                .Include(c => c.Animal)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Consulta consulta)
        {
            if (id != consulta.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Consulta");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao salvar: {ex.Message}");
                }
            }

            return View(consulta);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarClientePorId(int id)
        {
            var cliente = await _context.Clientes
                .Where(c => c.ClienteID == id)
                .Select(c => new { clienteID = c.ClienteID, nome = c.Nome })
                .FirstOrDefaultAsync();

            return Json(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarAnimalPorId(int id)
        {
            var animal = await _context.Animais
                .Where(a => a.AnimalID == id)
                .Select(a => new { animalID = a.AnimalID, nome = a.Nome })
                .FirstOrDefaultAsync();

            return Json(animal);
        }

        private bool ConsultaExists(int id)
        {
            return _context.Consultas.Any(e => e.Id == id);
        }

        [HttpDelete]
        public async Task<IActionResult> Excluir(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();

            return RedirectToAction("Consulta");
        }

    }




}