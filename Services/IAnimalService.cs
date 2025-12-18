using LH_PET.Models;
namespace LH_PET.Services
{
    public interface IAnimalService
    {
        Task<List<Animal>> GetAllAsync();
        Task<Animal?> GetByIdAsync(int id);
        Task<List<Animal>> SearchAsync(string busca);
        Task AddAsync(Animal animal);
        Task UpdateAsync(Animal animal);
        Task RemoveAsync(int id);
    }
}
