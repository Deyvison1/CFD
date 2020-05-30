using System;
namespace CFD.Dominio
{
    public class User
    {
        public int Id       { get; set; }
        public string Nome  { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public User()
        {
        }

        public User(int id, string nome, string email, string senha)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
        }
    }
}
