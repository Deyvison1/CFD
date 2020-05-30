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
        // Lista Todos Usuarios
        public async Task<User[]> GetAllUser()
        {
            return await _context.Users.OrderByDescending(x => x.Id).ToArrayAsync();
        }
        // Lista por ID
        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        // Listar por Nome ou Email
        public async Task<User[]> GetUserByNomeOrId(string buscar)
        {
            var result = _context.Users.Where(
                x => x.Nome.ToLower().Contains(buscar.ToLower()) || x.Email.ToLower().Contains(buscar.ToLower())
            );
            return await result.ToArrayAsync();
        }

    }
}
