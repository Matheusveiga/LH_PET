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

        // Exibir o formulário para agendar uma consulta
        [HttpGet]
        public async Task<IActionResult> Agendar()
        {
            // Obtendo os dados de clientes e animais do banco de dados
            var clientes = await _context.Clientes.ToListAsync();
            var animais = await _context.Animais.ToListAsync();

            if (clientes == null || animais == null)
            {
                return NotFound("Clientes ou Animais não encontrados.");
            }

            // Criando o ViewModel e preenchendo as listas de clientes e animais
            var viewModel = new ConsultaViewModel
            {
                Clientes = new SelectList(clientes, "Id", "Nome"),
                Animais = new SelectList(animais, "Id", "Nome")
            };

            // Retornando a view com o ViewModel preenchido
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Agendar(ConsultaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Consulta == null)
                {
                    viewModel.Consulta = new Consulta();
                }

                await _context.Consultas.AddAsync(viewModel.Consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }

            viewModel.Clientes = new SelectList(await _context.Clientes.ToListAsync(), "Id", "Nome", viewModel.Consulta?.ClienteId);
            viewModel.Animais = new SelectList(await _context.Animais.ToListAsync(), "Id", "Nome", viewModel.Consulta?.AnimalId);

            return View(viewModel);
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
    }
}