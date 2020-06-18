using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CFD.Dominio;

namespace CFD.WebAPI.Dtos
{
    public class UserDto
    {
        public int Id               { get; set; }
        public string Nome          { get; set; }
        public string Email         { get; set; }
        public string Senha         { get; set; }
        public int Papel            { get; set; }
        public List<Renda> Rendas   { get; set; }
        public List<Divida> Dividas { get; set; }   

    }
}
