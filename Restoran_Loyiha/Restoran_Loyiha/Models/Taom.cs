namespace Restoran_Loyiha.Models
{
    public class Taom
    {
        public int Id { get; set; }
        public string Nomi { get; set; }
        public string Tavsif { get; set; }
        public double Narx { get; set; }
        public string RasmUrl { get; set; }

      
        public int OshpazId { get; set; }
        public Oshpaz Oshpaz { get; set; }
        public ICollection<BuyurtmaElementlari> BuyurtmaElementlari { get; set; }
    }

}
