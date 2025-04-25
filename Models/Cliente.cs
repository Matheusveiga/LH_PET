using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LH_PET.Models
{

    [Table("Cliente")]
    public class Cliente 
    {

        [Key]
        public int ClienteID { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(80)]
        public string? CPF { get; set; }

        [Required]
        [StringLength(80)]
        public string? Email { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public ICollection<Animal> Animais { get; set; }

    }
}
