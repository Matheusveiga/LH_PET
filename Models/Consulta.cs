using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LH_PET.Models
{
    [Table("Consulta")]
    public class Consulta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }
        
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Required]
        public int AnimalId { get; set; }

        [ForeignKey("AnimalId")]
        public Animal Animal { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        public string? Descricao { get; set; }
    }
}
