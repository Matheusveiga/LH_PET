using LH_PET.Models;
using LH_PET.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LH_PET.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = new DashboardViewModel
            {
                TotalClientes = await _context.Clientes.CountAsync(),
                TotalAnimais = await _context.Animais.CountAsync(),
                TotalConsultas = await _context.Consultas.CountAsync(),
                TotalFornecedores = await _context.Fornecedores.CountAsync()
            };
            
            // Próximas 5 consultas
            var proximasConsultas = await _context.Consultas
                .Include(c => c.Animal)
                .Include(c => c.Cliente)
                .Where(c => c.DataHora >= DateTime.Now)
                .OrderBy(c => c.DataHora)
                .Take(5)
                .ToListAsync();
                
            dashboard.ProximasConsultas = proximasConsultas.Select(c => new ConsultaProximaViewModel
            {
                Id = c.Id,
                DataHora = c.DataHora,
                NomeAnimal = c.Animal?.Nome ?? "N/A",
                NomeCliente = c.Cliente?.Nome ?? "N/A",
                Descricao = c.Descricao ?? "",
                StatusCss = c.DataHora.Date == DateTime.Now.Date ? "danger" : 
                           c.DataHora.Date <= DateTime.Now.AddDays(2).Date ? "warning" : "info"
            }).ToList();
            
            // Animais por tipo
            var animaisPorTipo = await _context.Animais
                .GroupBy(a => a.Tipo)
                .Select(g => new { tipo = g.Key, quantidade = g.Count() })
                .OrderByDescending(x => x.quantidade)
                .ToListAsync();
                
            dashboard.TiposAnimais = animaisPorTipo.Select(x => x.tipo).ToList();
            dashboard.QuantidadesPorTipo = animaisPorTipo.Select(x => x.quantidade).ToList();
            
            // Consultas por descrição/tipo
            var consultasPorTipo = await _context.Consultas
                .GroupBy(c => c.Descricao ?? "Sem descrição")
                .Select(g => new { tipo = g.Key, quantidade = g.Count() })
                .OrderByDescending(x => x.quantidade)
                .Take(6)
                .ToListAsync();
                
            dashboard.TiposConsulta = consultasPorTipo.Select(x => x.tipo).ToList();
            dashboard.QuantidadesConsultaPorTipo = consultasPorTipo.Select(x => x.quantidade).ToList();
            
            // Animais recentes (últimos 5)
            var animaisRecentes = await _context.Animais
                .Include(a => a.Cliente)
                .OrderByDescending(a => a.DataCadastro)
                .Take(5)
                .ToListAsync();
                
            dashboard.AnimaisRecentes = animaisRecentes.Select(a => new AnimalRecenteViewModel
            {
                Id = a.AnimalID,
                Nome = a.Nome,
                Tipo = a.Tipo,
                Raca = a.Raca,
                NomeCliente = a.Cliente?.Nome ?? "N/A",
                DataCadastro = a.DataCadastro
            }).ToList();
            
            // Clientes recentes (últimos 5)
            var clientesRecentes = await _context.Clientes
                .Include(c => c.Animais)
                .OrderByDescending(c => c.DataCadastro)
                .Take(5)
                .ToListAsync();
                
            dashboard.ClientesRecentes = clientesRecentes.Select(c => new ClienteRecenteViewModel
            {
                Id = c.ClienteID,
                Nome = c.Nome,
                Email = c.Email,
                QuantidadeAnimais = c.Animais?.Count ?? 0,
                DataCadastro = c.DataCadastro
            }).ToList();
            
            return View(dashboard);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
