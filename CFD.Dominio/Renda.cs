using System;
namespace CFD.Dominio
{
    public class Renda
    {
        public int Id           { get; set; }
        public string Titulo    { get; set; }
        // 1 = Mensal, 2 = Diaria
        public int Tipo         { get; set; }
        public DateTime DataRenda    { get; set; }
        public decimal Valor    { get; set; }
        public string Descricao { get; set; }
        public int UserId       { get; set; }
        public User Usuario     { get; }
    }
}
