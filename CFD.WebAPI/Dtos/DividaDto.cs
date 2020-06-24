using System;
using System.ComponentModel.DataAnnotations;
using CFD.Dominio;

namespace CFD.WebAPI.Dtos
{
    public class DividaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [StringLength(170, MinimumLength = 8, ErrorMessage = "{0} deve ter entre 8 a 170 caracteres!")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCompra { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataRegistro { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataModificacao { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [Range(0, 1, ErrorMessage = "{0} deve ser 0 ou 1")]
        public int TipoDivida { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        public int Parcela { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [Range(0, 1, ErrorMessage = "{0} deve ser 0 ou 1.")]
        public int FormaCompra { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataVencimento { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Valor { get; set; }
        public string DescricaoProduto { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [Range(0,1, ErrorMessage = "{0} deve ser 0 ou 1.")]
        public int Situacao { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        public int UserId { get; set; }
    }
}
