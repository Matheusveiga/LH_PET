using LH_PET.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LH_PET.Models
{
    public class ConsultaViewModel
    {
        public Consulta Consulta { get; set; } = new Consulta();
        public SelectList Clientes { get; set; } = null!;
        public SelectList Animais { get; set; } = null!;
    }
}
