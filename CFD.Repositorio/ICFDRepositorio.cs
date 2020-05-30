using System;
using System.Threading.Tasks;
using CFD.Dominio;

namespace CFD.Repositorio
{
    public interface ICFDRepositorio
    {
        // ----> Gerais
        // Adicionar
        void Add<T>(T entidade) where T : class;
        // Atualizar
        void Update<T>(T entidade) where T : class;
        // Deletar
        void Delete<T>(T entidade) where T : class;
        Task<bool> SaveChanges();
        // ----> User
        // Listar Todos
        Task<User[]> GetAllUser();
        // Lista por ID
        Task<User> GetUserById(int id);
        // Listar por Nome ou Email
        Task<User[]> GetUserByNomeOrId(string buscar);

    }
}
