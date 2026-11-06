using System;
using System.ComponentModel.DataAnnotations;

namespace Restoran_Loyiha.Models
{
    public class Aloqa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "FIO majburiy")]
        public string FIO { get; set; }

        [Required(ErrorMessage = "Email majburiy")]
        [EmailAddress(ErrorMessage = "Email noto‘g‘ri")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Xabar bo‘sh bo‘lmasligi kerak")]
        public string Xabar { get; set; }

        public DateTime Sana { get; set; }
        
        public int? BuyurtmaId { get; set; }
        public Buyurtma? Buyurtma { get; set; }
    }
}
