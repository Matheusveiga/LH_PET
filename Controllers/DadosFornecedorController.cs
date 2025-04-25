using LH_PET.Context;
using LH_PET.Models;
using Microsoft.AspNetCore.Mvc;

namespace LH_PET.Controllers
{
    public class DadosFornecedorController : Controller
    {
        private readonly AppDbContext _context;

        public DadosFornecedorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult BuscarFornecedor(string busca)
        {
            var resultado = _context.Fornecedores.ToList();
            if (!string.IsNullOrEmpty(busca))
            {
                busca = busca.ToLower();

                resultado = _context.Fornecedores.Where(d => d.Nome.ToLower().Contains(busca) ||
                d.CNPJ.ToLower().Contains(busca) ||
                d.Email.ToLower().Contains(busca)).ToList();


            }
            return View(resultado);

        }
        [HttpGet]
        public IActionResult CreateFornecedor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFornecedor(Fornecedor fornecedor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(fornecedor);

                _context.Fornecedores.Add(fornecedor);
                _context.SaveChanges();

                return RedirectToAction("Buscar");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao criar fornecedor: " + ex.Message);
                return View(fornecedor);
            }
        }
    }
}
