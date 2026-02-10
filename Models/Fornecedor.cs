using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LH_PET.Models
{
    [Table("Fornecedor")]
    public class Fornecedor
    {
        [Key]
        public int FornecedorID { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(80, ErrorMessage = "Nome não pode ter mais de 80 caracteres")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "CNPJ é obrigatório")]
        [StringLength(18, ErrorMessage = "CNPJ inválido")]
        [ValidateCNPJ]
        public string? CNPJ { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(80, ErrorMessage = "Email não pode ter mais de 80 caracteres")]
        public string? Email { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
