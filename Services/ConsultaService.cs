using LH_PET.Context;
using LH_PET.Models;
using Microsoft.EntityFrameworkCore;

namespace LH_PET.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly AppDbContext _context;

        public ConsultaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Consulta consulta)
        {
            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Consulta>> GetAllAsync()
        {
            return await _context.Consultas
                .Include(c => c.Cliente)
                .Include(c => c.Animal)
                .ToListAsync();
        }

        public async Task<Consulta?> GetByIdAsync(int id)
        {
            return await _context.Consultas
                .Include(c => c.Cliente)
                .Include(c => c.Animal)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Consulta consulta)
        {
            _context.Consultas.Update(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
