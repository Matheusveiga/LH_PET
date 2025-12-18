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
        private readonly LH_PET.Services.IConsultaService _consultaService;
        private readonly LH_PET.Services.IClienteService _clienteService;
        private readonly LH_PET.Services.IAnimalService _animalService;

        public DadosConsultaController(LH_PET.Services.IConsultaService consultaService, LH_PET.Services.IClienteService clienteService, LH_PET.Services.IAnimalService animalService)
        {
            _consultaService = consultaService;
            _clienteService = clienteService;
            _animalService = animalService;
        }


        [HttpGet]
        public async Task<IActionResult> Agendar()
        {

            var clientes = await _clienteService.GetAllAsync() ?? new List<Cliente>();
            var animais = await _animalService.GetAllAsync() ?? new List<Animal>();


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
                    await _consultaService.AddAsync(consulta);
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
            var consultas = await _consultaService.GetAllAsync();

            return View(consultas);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarClientes(string term)
        {
            var clientes = await _clienteService.SearchNamesAsync(term);
            return Json(clientes);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarAnimais(string term)
        {
            var animais = await _animalService.SearchAsync(term);
            var list = animais.Select(a => new { animalID = a.AnimalID, nome = a.Nome }).ToList();
            return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var consulta = await _consultaService.GetByIdAsync(id);

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
                    await _consultaService.UpdateAsync(consulta);
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
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null) return Json(null);
            return Json(new { clienteID = cliente.ClienteID, nome = cliente.Nome });
        }

        [HttpGet]
        public async Task<IActionResult> BuscarAnimalPorId(int id)
        {
            var animal = await _animalService.GetByIdAsync(id);
            if (animal == null) return Json(null);
            return Json(new { animalID = animal.AnimalID, nome = animal.Nome });
        }

        private bool ConsultaExists(int id)
        {
            return false;
        }

        [HttpDelete]
        public async Task<IActionResult> Excluir(int id)
        {
            await _consultaService.RemoveAsync(id);
            return RedirectToAction("Consulta");
        }

    }




}