using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LH_PET.Models
{
    [Table("Fornecedor")]
    public class Fornecedor
    {
        [Key]
        public int FornecedorID { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(80)]
        public string? CNPJ { get; set; }

        [Required]
        [StringLength(80)]
        public string? Email { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;



    }
}
