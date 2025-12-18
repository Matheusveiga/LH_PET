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
        private readonly LH_PET.Services.IAnimalService _animalService;
        private readonly LH_PET.Services.IClienteService _clienteService;

        public DadosAnimalController(LH_PET.Services.IAnimalService animalService, LH_PET.Services.IClienteService clienteService)
        {
            _animalService = animalService;
            _clienteService = clienteService;
        }

        public async Task<ActionResult> BuscarAnimalAsync(string busca)
        {
            var resultado = await _animalService.GetAllAsync();
            if (!string.IsNullOrEmpty(busca))
            {
                busca = busca.ToLower();

                resultado = await _animalService.SearchAsync(busca);


            }
            return View(resultado);

        }

        [HttpGet]
        public async Task<JsonResult> BuscarClientesAsync(string termo)
        {
            var nomes = await _clienteService.SearchNamesAsync(termo);

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


                await _animalService.AddAsync(animais);

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
            var cliente = await _clienteService.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpGet]
        public async Task<ActionResult> EditarAnimalAsync(int id)
        {
            var animal = await _animalService.GetByIdAsync(id);
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

            await _animalService.UpdateAsync(animal);

            return RedirectToAction("Detalhes", "DadosAnimal", new { id = animal.ClienteID });
        }

        [HttpGet]
        public async Task<ActionResult> ExcluirAnimalAsync(int id)
        {
            var animal = await _animalService.GetByIdAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            int clienteId = animal.ClienteID;

            await _animalService.RemoveAsync(id);

            return RedirectToAction("Detalhes", new { id = clienteId });
        }


    }
}
