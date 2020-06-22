using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CFD.Dominio;
using CFD.Repositorio;
using CFD.WebAPI.Dtos;

namespace CFD.WebAPI._services
{
    public class DividaService
    {
        public readonly ICFDRepositorio _repo;
        public readonly IMapper _map;

        public DividaService(ICFDRepositorio repo, IMapper mapper) { _repo = repo; _map = mapper; }
        // Listar Ultimas Dividas
        public async Task<DividaDto[]> GetUltimasDividasAdd ()
        {
            try {
                var todasDividas = await _repo.GetAllDivida();

                var ultimasDividas = todasDividas.OrderByDescending(
                    x => x.Id
                ).Take(3);
                
                var dividasDto = _map.Map<DividaDto[]>(ultimasDividas);
                
                return dividasDto.ToArray();
            }catch(System.Exception e) {
                throw new ArgumentException($"DIVIDA: Erro ao listar ultimas. CODE: {e.Message}");
            }
        }

        // Listar Todos Por Paginacao
        public async Task<DividaDto[]> GetAllDividaPage(int page)
        {
            try
            {
                var dividaPage = await _repo.GetAllDividaPaginacao(page);

                var result = _map.Map<DividaDto[]>(dividaPage);

                return result.ToArray();

            }catch(System.Exception e)
            {
                throw new ArgumentException($"DIVIDA: Erro ao listar todos por paginacao. CODE: {e.Message}");
            }
        }
        // Listar Todos 
        public async Task<DividaDto[]> GetAllDivida() {
            try {
                var divida = await _repo.GetAllDivida();

                var result = _map.Map<DividaDto[]>(divida);

                return result.ToArray();

            } catch(System.Exception e) {
                throw new ArgumentException($"DIVIDA: Erro no Listar Todos. CODE: {e.Message}");
            }
        }
        // Listar Por ID
        public async Task<DividaDto> GetDividaById(int id) {
            try {
                var divida = await _repo.GetDividaById(id);
                if(divida == null) throw new ArgumentException("DIVIDA: Nenhum registro foi encontrado com esse Id");

                var result = _map.Map<DividaDto>(divida);

                return result;
            } catch(System.Exception e) {
                throw new ArgumentException($"DIVIDA: Erro ao Listar por id. CODE: {e.Message}");
            }
        }
        // Listar Por Titulo ou Descricao do Produto ou Valor
        public async Task<DividaDto[]> GetDividaByTituloOrDescricaoProdutoOrValor(string buscar) {
            try {
                var divida = await _repo.GetDividaByTituloOrDescricaoProdutoOrValor(buscar);
            
                var result = _map.Map<DividaDto[]>(divida);

                return result;
            } catch(System.Exception e) {
                throw new ArgumentException($"DIVIDA: Erro ao listar por nome ou descricao ou valor. CODE: {e.Message}");
            }
        }
        // Adicionar
        public async Task<DividaDto> Add(DividaDto dividaDto) {
            try {
                var entidade = _map.Map<Divida>(dividaDto);

                _repo.Add(entidade);

                if(await _repo.SaveChanges()) {
                    return _map.Map<DividaDto>(entidade);
                } else {
                    throw new ArgumentException("DIVIDA: Erro ao adicionar");
                }
            } catch(System.Exception e) {
                throw new ArgumentException($"DIVIDA: Erro ao adicionar. CODE: {e.Message}");
            }
        }
        // Atualizar
        public async Task<Divida> Update(DividaDto dividaDto) {
            try {
                var divida = await _repo.GetDividaById(dividaDto.Id);
                if(divida == null) throw new ArgumentException("DIVIDA: Nenhum registro encontrado com esse id");

                _map.Map(dividaDto, divida);
                _repo.Update(divida);

                if(await _repo.SaveChanges()) {
                    return divida;
                } else {
                    throw new ArgumentException("DIVIDA: Erro ao atualizar");
                }

            } catch(System.Exception e) {
                throw new ArgumentException($"DIVIDA: Erro no atualizar. CODE: {e.Message}");
            }
        }
        // Deletar
        public async Task<Divida> Delete(int id) {
            try {
                var divida = await _repo.GetDividaById(id);
                if(divida == null) throw new ArgumentException("DIVIDA: Nenhum registro encontrado com esse id");

                _repo.Delete(divida);

                if(await _repo.SaveChanges()) {
                    return divida;
                } else {
                    throw new ArgumentException("DIVIDA: Erro ao atualizar");
                }

            }catch(System.Exception e) {
                throw new ArgumentException($"DIVIDA: Erro no deletar. CODE: {e.Message}");
            }
        }
        // Valores Dividas por Usuario
        public async Task<DividaValoresDto> ValorTotal(int idUser)
        {
            try
            {

                var todasDividas = await _repo.GetAllDivida();
                var todasRendas = await _repo.GetAllRenda();
                // Rendas
                decimal totalRendas = todasRendas.Where(x => x.UserId == idUser).Sum(x => x.Valor);
                // Dividas
                decimal dividasPagas = todasDividas.Where(
                    x => x.UserId == idUser && x.Situacao == 1
                ).Sum(
                    x => x.Valor
                );
                decimal todasDividasPendentes = todasDividas.Where(
                    x => x.UserId == idUser && x.Situacao == 0
                ).Sum(
                    x => x.Valor
                );
                decimal todasDividasPorIdUser = todasDividas.Where(
                    x => x.UserId == idUser
                ).Sum(x => x.Valor);
                DividaValoresDto dividaValoresDto = new DividaValoresDto();

                dividaValoresDto.ValorTotalDividasPorId = todasDividasPorIdUser;
                dividaValoresDto.ValorTotalDividasPagas = dividasPagas;
                dividaValoresDto.ValorTotalDividasPendentes = todasDividasPendentes;
                dividaValoresDto.RendaBruta = totalRendas;
                dividaValoresDto.RendaLiquida = totalRendas - todasDividasPendentes;


                return dividaValoresDto;
            } catch(System.Exception e)
            {
                throw new ArgumentException($"DIVIDA: Erro ao listar valor total da divida por id. CODE: {e.Message}");
            }
        }

    }
}