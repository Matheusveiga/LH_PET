using LH_PET.Models;
namespace LH_PET.Services
{
    public interface IConsultaService
    {
        Task AddAsync(Consulta consulta);
        Task<List<Consulta>> GetAllAsync();
        Task<Consulta?> GetByIdAsync(int id);
        Task UpdateAsync(Consulta consulta);
        Task RemoveAsync(int id);
    }
}
