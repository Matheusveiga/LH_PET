using LH_PET.Context;
using LH_PET.Models;
using Microsoft.EntityFrameworkCore;

namespace LH_PET.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly AppDbContext _context;

        public AnimalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Animal animal)
        {
            await _context.Animais.AddAsync(animal);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Animal>> GetAllAsync()
        {
            return await _context.Animais.ToListAsync();
        }

        public async Task<Animal?> GetByIdAsync(int id)
        {
            return await _context.Animais.FindAsync(id);
        }

        public async Task<List<Animal>> SearchAsync(string busca)
        {
            busca = (busca ?? string.Empty).ToLower();
            return await _context.Animais
                .Where(a => (a.Nome ?? string.Empty).ToLower().Contains(busca) ||
                            (a.Tipo ?? string.Empty).ToLower().Contains(busca) ||
                            (a.Raca ?? string.Empty).ToLower().Contains(busca) ||
                            (a.Idade ?? string.Empty).ToLower().Contains(busca))
                .ToListAsync();
        }

        public async Task UpdateAsync(Animal animal)
        {
            _context.Animais.Update(animal);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var animal = await _context.Animais.FindAsync(id);
            if (animal != null)
            {
                _context.Animais.Remove(animal);
                await _context.SaveChangesAsync();
            }
        }
    }
}
