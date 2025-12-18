using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LH_PET.Models
{
    [Table("Consulta")]
    public class Consulta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteID { get; set; }

        [ForeignKey("ClienteID")]
        [ValidateNever]
        public Cliente? Cliente { get; set; }

        [Required]
        public int AnimalID { get; set; }

        [ForeignKey("AnimalID")]
        [ValidateNever]
        public Animal? Animal { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        public string? Descricao { get; set; }
    }
}
