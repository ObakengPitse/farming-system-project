using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace farming_system_project.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Category { get; set; }
        [Required]
        public DateTime ProductionDate { get; set; }
        [Required]
        public int FarmerId { get; set; }
        public Farmer Farmer { get; set; }
    }
}
