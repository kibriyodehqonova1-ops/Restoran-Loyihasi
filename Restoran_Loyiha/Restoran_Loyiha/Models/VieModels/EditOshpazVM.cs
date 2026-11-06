namespace Restoran_Loyiha.Models.ViewModels
{
    public class EditOshpazVM
    {
        public int Id { get; set; }

        public string Ism { get; set; }
        public string Familiya { get; set; }

        public string Lavozim { get; set; }
        public int TajribaYillari { get; set; }

        public string EskiRasmUrl { get; set; }
        public IFormFile YangiRasm { get; set; }
    }
}
