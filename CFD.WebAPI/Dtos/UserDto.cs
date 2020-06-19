using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CFD.Dominio;

namespace CFD.WebAPI.Dtos
{
    public class UserDto
    {
        public int Id               { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "{0} deve ter entre 2 a 60 caracteres!")]
        public string Nome          { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [EmailAddress]
        public string Email         { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} deve ter entre 4 a 20 caracteres!")]
        public string Senha         { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio!")]
        [Range(1,2, ErrorMessage = "{0} so pode ser 1 ou 2.")]
        public int Papel            { get; set; }
        public List<Renda> Rendas   { get; set; }
        public List<Divida> Dividas { get; set; }   

    }
}
