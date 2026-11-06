using System.ComponentModel.DataAnnotations;

namespace Restoran_Loyiha.Models.ViewModels
{
    public class CreateMenyuVM
    {
        [Required(ErrorMessage = "Taom nomini kiriting")]
        public string Nomi { get; set; }

        [Required(ErrorMessage = "Narxni kiriting")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Narx 0 dan katta bo‘lishi kerak")]
        public double Narx { get; set; }

        [Required(ErrorMessage = "Tavsif kiriting")]
        public string Tavsif { get; set; }

        public IFormFile RasmUrl { get; set; }

        [Required(ErrorMessage = "Oshpazni tanlang")]
        public int OshpazId { get; set; }
    }
}