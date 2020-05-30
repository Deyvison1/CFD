using System;
namespace CFD.WebAPI.Dtos
{
    public class UserDto
    {
        public int Id       { get; set; }
        public string Nome  { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

    }
}
