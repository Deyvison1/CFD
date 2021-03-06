﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CFD.Dominio;
using CFD.Repositorio;
using CFD.WebAPI.Dtos;

namespace CFD.WebAPI._services
{
    public class RendaService
    {
        private readonly ICFDRepositorio _repo;
        private readonly IMapper _map;
        public RendaService(ICFDRepositorio repo, IMapper map) { _repo = repo; _map = map; }
        // Listar Por UserId
        public async Task<RendaDto[]> GetAllRendaByUserId(int id) {
            try {
                var rendaByUserId = await _repo.GetAllRendaByUserId(id);

                var rendaByUserIdDto = _map.Map<RendaDto[]>(rendaByUserId);

                return rendaByUserIdDto.ToArray();
            }catch(System.Exception e) {
                throw new ArgumentException($"RENDA: Erro ao listar por UserId. CODE: {e.Message}");
            }
        }
        // Listar Ultimos Adicionados Por Id
        public async Task<RendaDto[]> GetUltimosRendaAddById(int id) 
        {
            try
            {
                var todasRendas = await _repo.GetAllRendaByUserId(id);

                var lastRendasAdd = todasRendas.OrderByDescending(
                    x => x.Id
                ).Take(3);

                var lastRendaDto = _map.Map<RendaDto[]>(lastRendasAdd);

                return lastRendaDto.ToArray();
            }
            catch(System.Exception e)
            {
                throw new ArgumentException($"RENDA: Erro ao listar ultimos. CODE: {e.Message}");
            }
        }
        // Listar Ultimos Adicionados
        public async Task<RendaDto[]> GetUltimosRendaAdd() 
        {
            try
            {
                var todasRendas = await _repo.GetAllRenda();

                var lastRendasAdd = todasRendas.OrderByDescending(
                    x => x.Id
                ).Take(3);

                var lastRendaDto = _map.Map<RendaDto[]>(lastRendasAdd);

                return lastRendaDto.ToArray();
            }
            catch(System.Exception e)
            {
                throw new ArgumentException($"RENDA: Erro ao listar ultimos. CODE: {e.Message}");
            }
        }
        // Listar Todos por Renda PAGE
        public async Task<RendaDto[]> GetAllRendapPage(int page) 
        {
            try {
                var rendaPage = await _repo.GetAllRendaPaginacao(page);

                var result = _map.Map<RendaDto[]>(rendaPage);

                return result.ToArray();
            }catch(System.Exception e) {
                throw new ArgumentException($"RENDA: Erro ao listar todos por paginacao. CODE: {e.Message}");
            }
        }
        // LISTAR TODOS POR ID
        public async Task<RendaDto[]> GetAllRendabById(int id)
        {
            try
            {
                var renda = await _repo.GetAllRendaByUserId(id);

                var result = _map.Map<RendaDto[]>(renda);

                return result.ToArray();

            } catch(System.Exception e)
            {
                throw new ArgumentException($"Renda: Erro ao listar todos. CODE: {e.Message}");
            }
        }
        // Listar todos
        public async Task<RendaDto[]> GetAllRenda()
        {
            try
            {
                var renda = await _repo.GetAllRenda();

                var result = _map.Map<RendaDto[]>(renda);

                return result.ToArray();

            } catch(System.Exception e)
            {
                throw new ArgumentException($"Renda: Erro ao listar todos. CODE: {e.Message}");
            }
        }
        // Listar por Id
        public async Task<RendaDto> GetRendaById(int id)
        {
            try
            {
                var renda = await _repo.GetRendaById(id);
                if (renda == null) throw new ArgumentException("Nenhum registro encontrado com esse id");
                var result = _map.Map<RendaDto>(renda);

                return result;

            }
            catch (System.Exception e)
            {
                throw new ArgumentException($"Renda: Erro ao listar por Id. CODE: {e.Message}");
            }
        }
        // Listar por Titulo ou Valor ou Descricao
        public async Task<RendaDto[]> GetRendaByTituloOrValorOrDescricao(string buscar)
        {
            try
            {
                var renda = await _repo.GetRendaByTituloOrValorOrDescricao(buscar);

                var result = _map.Map<RendaDto[]>(renda);

                return result;
            }
            catch (System.Exception e)
            {
                throw new ArgumentException($"Renda: Erro ao listar todos. CODE: {e.Message}");
            }
        }
        // Adicionar
        public async Task<RendaDto> Add(RendaDto rendaDto)
        {
            try
            {
                var entidade = _map.Map<Renda>(rendaDto);

                _repo.Add(entidade);

                if(await _repo.SaveChanges())
                {
                    return _map.Map<RendaDto>(entidade);
                }
                else
                {
                    throw new ArgumentException("Renda: Erro ao Adicionar");
                }
            }
            catch (System.Exception e)
            {
                throw new ArgumentException($"Renda: Erro ao adicionar. CODE: {e.Message}");
            }
        }
        // Atualizar
        public async Task<RendaDto> Update(RendaDto rendaDto)
        {
            try
            {
                var renda = await _repo.GetRendaById(rendaDto.Id);
                if (renda == null) throw new ArgumentException("RENDA: Nenhum registro encontrado com ese id");

                _map.Map(rendaDto, renda);
                _repo.Update(renda);

                if(await _repo.SaveChanges())
                {
                    return _map.Map<RendaDto>(renda);
                }
                else
                {
                    throw new ArgumentException("RENDA: Erro ao atualizar.");
                }
            }
            catch (System.Exception e)
            {
                throw new ArgumentException($"Renda: Erro ao atualizar. CODE: {e.Message}");
            }
        }
        // Deletar
        public async Task<Renda> Delete(int id)
        {
            try
            {
                var renda = await _repo.GetRendaById(id);
                if (renda == null) throw new ArgumentException("RENDA: Nenhum registro encontrado com esse id");

                _repo.Delete(renda);

                if(await _repo.SaveChanges())
                {
                    return renda;
                }
                else
                {
                    throw new ArgumentException("RENDA: Erro ao deletar");
                }
            }
            catch (System.Exception e)
            {
                throw new ArgumentException($"Renda: Erro ao deletar. CODE: {e.Message}");
            }
        }
    }
}
