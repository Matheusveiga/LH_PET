using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LH_PET.Models
{

    [Table("Cliente")]
    public class Cliente 
    {

        [Key]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(80, ErrorMessage = "Nome não pode ter mais de 80 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(14, ErrorMessage = "CPF inválido")]
        [ValidateCPF]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(80, ErrorMessage = "Email não pode ter mais de 80 caracteres")]
        public string Email { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public ICollection<Animal> Animais { get; set; } = new List<Animal>();

    }
}
