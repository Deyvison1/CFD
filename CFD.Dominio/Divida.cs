using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CFD.Dominio
{
    public class Divida
    {
        public int Id                    { get; set; }
        public string Titulo             { get; set; }
        public DateTime DataCompra       { get; set; }
        public DateTime? DataRegistro    { get; set; }
        public DateTime? DataModificacao { get; set; }
        public int Parcela               { get; set; }
        public int FormaCompra           { get; set; }
        public DateTime? DataVencimento  { get; set; }
        public decimal ValorParcela      { get; set; }
        public decimal ValorTotal        { get; set; }
        public string DescricaoProduto   { get; set; }
        public int Situacao              { get; set; }
        public int UserId                { get; set; }
        public User Usuario              { get; set; }

    }
}
