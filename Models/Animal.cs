using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LH_PET.Models
{
    [Table("Animal")]
    public class Animal
    {
        [Key]
        public int AnimalID { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(80)]
        public string? Tipo { get; set; }

        [Required]
        [StringLength(80)]
        public string? Sexo { get; set; }

        [Required]
        [StringLength(80)]
        public string? Raca { get; set; }

        [Required]
        [StringLength(80)]
        public string? Idade { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public int ClienteID { get; set; } // Chave estrangeira
        
        [ValidateNever]
        public required Cliente Cliente { get; set; } // Navegação

    }
}