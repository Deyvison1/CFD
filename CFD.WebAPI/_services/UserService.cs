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

        public UserService(ICFDRepositorio repo, IMapper mapper, CFDContext context) { 
            _repo = repo; _map = mapper;
        }

        // Listar com Paginacao
        public UserDto[] GetAllUserPaginacao() {
            try {
                var user = _repo.GetAllUSerPaginacao(2);
                var result = _map.Map<UserDto[]>(user);

                return result.ToArray();                
            }catch(System.Exception e) {
                throw new ArgumentException($"USER: Erro no Listar por Paginacao. CODE: {e.Message}");
            }
        }
        // Lista Todos
        public async Task<UserDto[]> GetAllUser()
        {
            try{
                var user = await _repo.GetAllUser();
                //var user = await _repo.GetAllUser();

                var result = _map.Map<UserDto[]>(user);
                
                return result.ToArray();
                
            } catch(System.Exception e) {
                throw new ArgumentException($"USER: Erro ao listar todos. CODE: {e.Message}");
            }
        }
        // Listar por Id com as Rendas e Dividas
        public async Task<UserDto[]> GetUserByIdAndDividasAndRendas(int id)
        {
            try
            {
                var userByIdAndRendasAndDividas = await _repo.GetUserByIdAndDividasAndRendas(id);

                var result = _map.Map<UserDto[]>(userByIdAndRendasAndDividas);

                return result.ToArray();

            } catch(System.Exception e)
            {
                throw new ArgumentException($"Erro ao listar por id com rendas e dividas. CODE: {e.Message}");
            }
        }
        // Listar por ID
        public async Task<UserDto> GetUserById(int id)
        {
            try
            {
                var user = await _repo.GetUserById(id);
                if(user == null) throw new ArgumentException("USER: Nenhum registro encontrado com esse id.");
                var result = _map.Map<UserDto>(user);

                return result;

            } catch (System.Exception e)
            {
                throw new ArgumentException($"USER: Erro ao listar por id. CODE: {e.Message}");
            }

        }
        // Listar Por Nome ou Email ou Papel
        public async Task<UserDto[]> GetUserByNomeOrIdOrPapel(string buscar) {
            try {
                var user = await _repo.GetUserByNomeOrIdOrPapel(buscar);

                var result = _map.Map<UserDto[]>(user);

                return result;

            } catch(System.Exception e) {
                throw new ArgumentException($"USER: Erro ao Buscar. CODE: {e.Message}");
            }
        }
        // Adicionar
        public async Task<UserDto> Add(UserDto user)
        {
            try {
            var emailExiste = await _repo.GetUserByEmailExist(user.Email);
            if (emailExiste != null) throw new ArgumentException("USER: Ja existe um usuario cadastrado com esse e-mail.");

            var entidade = _map.Map<User>(user);

            _repo.Add(entidade);

            if(await _repo.SaveChanges())
            {
                return _map.Map<UserDto>(entidade);
            }
            else
            {
                throw new ArgumentException("USER: Erro ao Adicionar.");
            }
            } catch(System.Exception e) {
                throw new ArgumentException($"USER: Erro ao Adicionar. CODE: {e.Message}");
            }
        }

        // Atualizar
        public async Task<User> Update(UserDto userDto) 
        {
            try {
                var user = await _repo.GetUserById(userDto.Id);
                if(user == null) throw new ArgumentException("USER: Nenhum registro encontrado com esse id"); 
                
                _map.Map(userDto, user);
                _repo.Update(user);

                if(await _repo.SaveChanges()) {
                    return user;
                } else {
                    throw new ArgumentException("USER: Erro no Atualizar");
                }

            } catch(System.Exception e) {
                throw new ArgumentException($"USER: Erro ao atualizar. CODE: {e.Message}");
            }
        }
        // Deletar
        public async Task<User> Delete(int id) {
            try {
                var user = await _repo.GetUserById(id);
                if(user == null) throw new ArgumentException("USER: Nenhum registro encontrado com esse id");

                _repo.Delete(user);

                if(await _repo.SaveChanges()) {
                    return user;
                } else {
                    throw new ArgumentException("Erro ao deletar");
                }

            }catch(System.Exception e) {
                throw new ArgumentException($"USER: Erro ao Deletar. CODE: {e.Message}");
            }
        }
    }
}