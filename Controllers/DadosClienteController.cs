using LH_PET.Context;
using LH_PET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LH_PET.Controllers
{
    [Authorize]
    public class DadosClienteController : Controller
    {

        private readonly LH_PET.Services.IClienteService _clienteService;

        public DadosClienteController(LH_PET.Services.IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult> Buscar(string busca)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(busca))
                {
                    var todos = await _clienteService.GetAllAsync();
                    return View(todos);
                }

                busca = busca.ToLower();

                var resultado = await _clienteService.SearchAsync(busca);

                return View(resultado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar recuperar os clientes.");
            }


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(cliente);
                }

                await _clienteService.AddAsync(cliente);

                return RedirectToAction("Buscar");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao criar cliente: " + ex.Message);
                return View(cliente);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            // Obtém o cliente com o ID fornecido, incluindo os animais
            var cliente = await _clienteService.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();  // Retorna 404 caso o cliente não seja encontrado
            }

            return View(cliente);  // Retorna a view com os dados do cliente
        }

    }
}
