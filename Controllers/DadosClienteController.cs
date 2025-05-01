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

        private readonly AppDbContext _context;

        public DadosClienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Buscar(string busca)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(busca))
                {
                    var todos = _context.Clientes.ToList();
                    return View(todos);
                }

                busca = busca.ToLower();

                var resultado = _context.Clientes
                    .Where(c =>
                        c.Nome.ToLower().Contains(busca) ||
                        c.CPF.ToLower().Contains(busca) ||
                        c.Email.ToLower().Contains(busca) ||
                        c.DataCadastro.ToString().Contains(busca))
                    .ToList();

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
        public IActionResult Create(Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(cliente);
                }


                _context.Clientes.Add(cliente);
                _context.SaveChanges();

                return RedirectToAction("Buscar");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao criar cliente: " + ex.Message);
                return View(cliente);
            }

        }

        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            // Obtém o cliente com o ID fornecido, incluindo os animais
            var cliente = _context.Clientes
                .Include(c => c.Animais)  // Inclui os animais relacionados ao cliente
                .FirstOrDefault(c => c.ClienteID == id);

            if (cliente == null)
            {
                return NotFound();  // Retorna 404 caso o cliente não seja encontrado
            }

            return View(cliente);  // Retorna a view com os dados do cliente
        }

    }
}
