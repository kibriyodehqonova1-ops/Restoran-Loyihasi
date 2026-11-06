namespace Restoran_Loyiha.Models
{
    public class BuyurtmaElementlari
    {
        public int Id { get; set; }

        public int BuyurtmaId { get; set; }
        public Buyurtma Buyurtma { get; set; }

        public int TaomId { get; set; }
        public Taom Taom { get; set; }

        public int Miqdor { get; set; }

        public decimal UmumiyNarx { get; set; }  
    }

}
