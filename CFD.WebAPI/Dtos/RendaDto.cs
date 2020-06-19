using System.ComponentModel.DataAnnotations;
using CFD.Dominio;

namespace CFD.WebAPI.Dtos
{
    public class RendaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [StringLength(170, MinimumLength = 2, ErrorMessage = "{0} deve ter entre 2 a 170 caracteres.")]
        public string Titulo    { get; set; }
        // 0 = Mensal, 1 = Diaria
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [Range(0,1, ErrorMessage = "{0} deve ser 0 ou 1.")]
        public int Tipo         { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Valor     { get; set; }
        public string Descricao { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        public int UserId       { get; set; }
    }
}