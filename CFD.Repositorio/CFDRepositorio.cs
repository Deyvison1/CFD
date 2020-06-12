using System;
using System.Linq;
using System.Threading.Tasks;
using CFD.Dominio;
using Microsoft.EntityFrameworkCore;

namespace CFD.Repositorio
{
    public class CFDRepositorio : ICFDRepositorio
    {
        private readonly CFDContext _context;
        public CFDRepositorio(CFDContext context)
        {
            _context = context;
        }
        // ---> Adicionar
        public void Add<T>(T entidade) where T : class
        {
            _context.Add(entidade);
        }
        // ---> Atualizar
        public void Update<T>(T entidade) where T : class
        {
            _context.Update(entidade);
        }
        // ---> Deletar
        public void Delete<T>(T entidade) where T : class
        {
            _context.Remove(entidade);
        }
        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        // -----> User
        // Lista Todos Usuarios
        public async Task<User[]> GetAllUser()
        {
            IQueryable<User> query = _context.Users.Include(x => x.Dividas);

            query = query.OrderByDescending(x => x.Id);
            return await query.ToArrayAsync();
        }
        // Lista User Por ID
        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        // Listar User Por Nome Ou Email
        public async Task<User[]> GetUserByNomeOrIdOrPapel(string buscar)
        {
            var result = _context.Users.Where(
                x => x.Nome.ToLower().Contains(buscar.ToLower()) || x.Email.ToLower().Contains(buscar.ToLower())
            );
            return await result.ToArrayAsync();
        }
        // Verificar Se Ja Existe Algum User Com O Mesmo E-mail Cadastrado.
        public async Task<User> GetUserByEmailExist(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(
                    x => x.Email == email
                );
        }


        // -----> Divida
        // Lista Todas Dividas
        public async Task<Divida[]> GetAllDivida()
        {
            return await _context.Dividas.OrderByDescending(x => x.Id).ToArrayAsync();
        }
        // Listar Divida Por ID
        public async Task<Divida> GetDividaById(int id)
        {
            return await _context.Dividas.FirstOrDefaultAsync(x => x.Id == id);
        }
        //Listar Divida Por Titulo Ou Descricao Do Produto Ou Valor
        public async Task<Divida[]> GetDividaByTituloOrDescricaoProdutoOrValor(string buscar)
        {
            var result = _context.Dividas.Where(
                x => x.Titulo.ToLower().Contains(buscar.ToLower()) ||
                x.DescricaoProduto.ToLower().Contains(buscar.ToLower()) ||
                x.Valor.ToString().Contains(buscar)
            );

            return await result.ToArrayAsync();
        }
    }
}
