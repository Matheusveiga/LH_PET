using LH_PET.Context;
using LH_PET.Models;
using Microsoft.EntityFrameworkCore;

namespace LH_PET.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Animais)
                .FirstOrDefaultAsync(c => c.ClienteID == id);
        }

        public async Task<List<Cliente>> SearchAsync(string busca)
        {
            busca = (busca ?? string.Empty).ToLower();
            return await _context.Clientes
                .Where(c => (c.Nome ?? string.Empty).ToLower().Contains(busca) ||
                            (c.CPF ?? string.Empty).ToLower().Contains(busca) ||
                            (c.Email ?? string.Empty).ToLower().Contains(busca) ||
                            c.DataCadastro.ToString().Contains(busca))
                .ToListAsync();
        }

        public async Task<List<object>> SearchNamesAsync(string termo)
        {
            termo = (termo ?? string.Empty).ToLower();
            var result = await _context.Clientes
                .Where(c => (c.Nome ?? string.Empty).ToLower().Contains(termo))
                .Select(c => new { id = c.ClienteID, nome = c.Nome })
                .ToListAsync();

            return result.Cast<object>().ToList();
        }
    }
}
