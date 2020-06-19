using System;
using System.ComponentModel.DataAnnotations;
namespace CFD.WebAPI.Dtos
{
    public class DividaValoresDto
    {
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ValorTotalDividasPorId     { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ValorTotalDividasPagas     { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ValorTotalDividasPendentes { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal RendaBruta                 { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal RendaLiquida               { get; set; }
    }
}
