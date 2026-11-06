namespace Restoran_Loyiha.Models
{
    public class Oshpaz
    {
        public int Id { get; set; }
        public string Ism { get; set; }
        public string Familiya { get; set; }
        public string Lavozim { get; set; }
        public int TajribaYillari { get; set; }
        public string RasmUrl { get; set; }

        public ICollection<Taom> Taomlar { get; set; }
    }

}
