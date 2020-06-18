using System;
namespace CFD.WebAPI.Dtos
{
    public class DividaValoresDto
    {
        public double ValorTotalDividasPorId     { get; set; }
        public double ValorTotalDividasPagas     { get; set; }
        public double ValorTotalDividasPendentes { get; set; }
        public double RendaTotal                 { get; set; }
        public double RendaLiquida               { get; set; }
    }
}
