using static Restoran_Loyiha.Models.Buyurtma;


namespace Restoran_Loyiha.Models.ViewModels
{
    public class BuyurtmaListVM
    {
        public int Id { get; set; }
        public string Mijoz { get; set; }

        public string Taom { get; set; }
        public string TaomRasm { get; set; }

        public BuyurtmaHolati Holat { get; set; }
        public decimal Summa { get; set; }
    }

}

