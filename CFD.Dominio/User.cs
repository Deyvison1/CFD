using System;
using System.Collections;
using System.Collections.Generic;

namespace CFD.Dominio
{
    public class User
    {
        public int Id               { get; private set; }
        public string Nome          { get; set; }
        public string Email         { get; set; }
        public string Senha         { get; private set; }
        public int Papel            { get; private set; }
        public List<Divida> Dividas { get; set; } = new List<Divida>();
        public List<Renda> Rendas   { get; set; } = new List<Renda>();

        /*
        public User()
        {
            this.Dividas = new List<Divida>();
        }
        */

    }
}
