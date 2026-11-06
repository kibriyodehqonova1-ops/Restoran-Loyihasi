using Microsoft.AspNetCore.Http;
using Restoran_Loyiha.Models;

namespace Restoran_Loyiha.Models.ViewModels
{
    public class CreateOshpazVM
    {
        public string Ism { get; set; }               
        public string Familiya { get; set; }         
        public IFormFile RasmUrl { get; set; }                      
        public string Lavozim { get; set; }
        public int TajribaYillari { get; set; }
        
    }
}
