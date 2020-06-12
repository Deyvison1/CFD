using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CFD.Dominio;
using CFD.Repositorio;
using CFD.WebAPI.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace CFD.WebAPI._services
{
    public class UserService
    {
        private readonly ICFDRepositorio _repo;
        private readonly IMapper _map;

        public UserService(ICFDRepositorio repo, IMapper mapper) { _repo = repo; _map = mapper; }

        // Lista Todos
        public async Task<UserDto[]> GetAllUser()
        {
            try{
                var user = await _repo.GetAllUser();

                var result = _map.Map<UserDto[]>(user);
                
                return result.ToArray();
                
            } catch(System.Exception e) {
                throw new ArgumentException("Erro no listar todos "+e);
            }
        }
        // Listar por ID
        public async Task<UserDto> GetUserById(int id)
        {
            try
            {
                var user = await _repo.GetUserById(id);
                if(user == null) throw new ArgumentException("Nenhum registro encontrado");
                var result = _map.Map<UserDto>(user);

                return result;

            } catch 
            {
                throw new ArgumentException("Nenhum registro encontrado");
            }

        }
        // Listar Por Nome ou Email ou Papel
        public async Task<UserDto[]> GetUserByNomeOrIdOrPapel(string buscar) {
            try {
                var user = await _repo.GetUserByNomeOrIdOrPapel(buscar);

                var result = _map.Map<UserDto[]>(user);

                return result;


            } catch(System.Exception e) {
                throw new ArgumentException($"Erro ao Buscar. CODE: {e.Message}");
            }
        }
        // Adicionar
        public async Task<UserDto> Add(UserDto user)
        {
            try {
            var emailExiste = await _repo.GetUserByEmailExist(user.Email);
            if (emailExiste != null) throw new ArgumentException("Email ja existe");

            var entidade = _map.Map<User>(user);

            _repo.Add(entidade);

            if(await _repo.SaveChanges())
            {
                return _map.Map<UserDto>(entidade);
            }
            else
            {
                throw new ArgumentException("Erro ao Adicionar.");
            }
            } catch(System.Exception e) {
                throw new ArgumentException($"Erro ao Adicionar.CODE: {e.Message}");
            }
        }

        // Atualizar
        public async Task<User> Update(UserDto userDto) 
        {
            try {
                var user = await _repo.GetUserById(userDto.Id);
                if(user == null) throw new ArgumentException("Nenhum registro encontrado"); 
                
                _map.Map(userDto, user);
                _repo.Update(user);

                if(await _repo.SaveChanges()) {
                    return user;
                } else {
                    throw new ArgumentException("Erro no Update");
                }

            } catch(System.Exception e) {
                throw new ArgumentException($"Erro. CODE: {e.Message}");
            }
        }
        // Deletar
        public async Task<User> Delete(int id) {
            try {
                var user = await _repo.GetUserById(id);
                if(user == null) throw new ArgumentException("Erro");

                _repo.Delete(user);

                if(await _repo.SaveChanges()) {
                    return user;
                } else {
                    throw new ArgumentException($"Erro");
                }

            }catch(System.Exception e) {
                throw new ArgumentException($"Erro. CODE: {e.Message}");
            }
        }



    }
}