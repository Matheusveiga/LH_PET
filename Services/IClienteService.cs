using LH_PET.Models;
namespace LH_PET.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<List<Cliente>> SearchAsync(string busca);
        Task<List<object>> SearchNamesAsync(string termo);
        Task AddAsync(Cliente cliente);
    }
}
