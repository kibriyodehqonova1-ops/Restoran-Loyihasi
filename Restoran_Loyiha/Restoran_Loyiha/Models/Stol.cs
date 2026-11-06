using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restoran_Loyiha.Models
{
    [Table("Stol")]
    public class Stol
    {
        public int Id { get; set; }

        public int StolRaqami { get; set; }

  
        public string RasmUrl { get; set; }

       
        public int Sigimi { get; set; }

        public bool Band { get; set; } = false;

       
        public List<Bron> Bronlar { get; set; } = new List<Bron>();
    }
}
