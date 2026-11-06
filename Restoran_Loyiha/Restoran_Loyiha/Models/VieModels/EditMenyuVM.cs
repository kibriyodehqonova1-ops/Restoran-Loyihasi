namespace Restoran_Loyiha.Models.ViewModels
{
    public class EditMenyuVM
    {
        public int Id { get; set; }          

        public string Nomi { get; set; }
       
        public double Narx { get; set; }
        public string Tavsif { get; set; }

        public int OshpazId { get; set; }    

        public string EskiRasmUrl { get; set; } 
        public IFormFile YangiRasm { get; set; } 
    }
}