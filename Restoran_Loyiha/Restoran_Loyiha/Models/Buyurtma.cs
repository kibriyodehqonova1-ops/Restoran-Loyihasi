using Microsoft.AspNetCore.Identity;

namespace Restoran_Loyiha.Models
{
    public class Buyurtma
    {
        public int Id { get; set; }
        public DateTime Sana { get; set; } = DateTime.Now;
        public BuyurtmaHolati Holat { get; set; } = BuyurtmaHolati.Ochiq;
        public enum BuyurtmaHolati { Ochiq, Tayyor, Yopilgan }
        public decimal Summa { get; set; }
       
        public int StolId { get; set; }
        public Stol Stol { get; set; }
        public ICollection<Aloqa> Aloqalar { get; set; }
     

        public ICollection<BuyurtmaElementlari> Elementlar { get; set; }
    }
}
