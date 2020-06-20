using System;
using System.Collections;
using System.Threading.Tasks;
using CFD.Dominio;
using X.PagedList;
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
        // Paginacao
        Task<User[]> GetAllUSerPaginacao(int page);
        // Listar Todos
        Task<User[]> GetAllUser();
        // Listar por Id para deletar e etc
        Task<User[]> GetUserByIdAndDividasAndRendas(int id);
        // Lista por ID
        Task<User> GetUserById(int id);
        // Listar por Nome ou Email
        Task<User[]> GetUserByNomeOrIdOrPapel(string buscar);
        // Lista por Email
        Task<User> GetUserByEmailExist(string email);

        // ----> Divida
        // Listar Todas por Paginacao
        Task<Divida[]> GetAllDividaPaginacao(int page);
        // Listar Todas com seus includes
        Task<Divida[]> GetAllDivida();
        // Listar por ID
        Task<Divida> GetDividaById(int id);
        // Listar por Titulo, Descricao do Produto, Valor
        Task<Divida[]> GetDividaByTituloOrDescricaoProdutoOrValor(string buscar);
        // ----> Renda
        // Listar Todas por Pginacao
        Task<Renda[]> GetAllRendaPaginacao(int page);
        // Listar Todas
        Task<Renda[]> GetAllRenda();
        // Listar por ID
        Task<Renda> GetRendaById(int id);
        // Listar por Titulo, Valor ou Descricao
        Task<Renda[]> GetRendaByTituloOrValorOrDescricao(string buscar);

    }
}
