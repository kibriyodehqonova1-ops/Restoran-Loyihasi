using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restoran_Loyiha.Models
{
    [Table("Bron")]
    public class Bron
    {
        public int Id { get; set; }

        [Required]
        public string MijozIsm { get; set; }

        [Required]
        public string Email { get; set; } 

        [Required]
        public DateTime Sana { get; set; }

        [Required]
        public TimeSpan Vaqt { get; set; }

        public string Holat { get; set; } = "Ochiq";

        [Required]
        public int StolId { get; set; }
        public Stol Stol { get; set; }
    }
}
