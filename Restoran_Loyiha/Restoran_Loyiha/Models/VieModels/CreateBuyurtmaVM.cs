using Restoran_Loyiha.Models.ViewModels;
using static Restoran_Loyiha.Models.Buyurtma;

namespace Restoran_Loyiha.Models.ViewModels
{
    public class CreateBuyurtmaVM
    {
        public int Id { get; set; } 
        public string? Mijoz { get; set; }
        public int StolId { get; set; }
        public decimal Summa { get; set; }
        public  BuyurtmaHolati Holat { get; set; }
        public List<CreateBuyurtmaItemVM> Elementlar { get; set; } = new List<CreateBuyurtmaItemVM>();
    }


}