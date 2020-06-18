using CFD.Dominio;

namespace CFD.WebAPI.Dtos
{
    public class RendaDto
    {
        public int Id           { get; set; }
        public string Titulo    { get; set; }
        // 0 = Mensal, 1 = Diaria
        public int Tipo         { get; set; }
        public double Valor     { get; set; }
        public string Descricao { get; set; }
        public int UserId       { get; set; }
    }
}