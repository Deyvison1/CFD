using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CFD.Dominio;
using CFD.Repositorio;
using CFD.WebAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CFD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DividaController : ControllerBase
    {
        private readonly ICFDRepositorio _repo;
        private readonly IMapper _map;
        public DividaController(ICFDRepositorio repo, IMapper mapper) { _repo = repo; _map = mapper; }

        // Lista Todos
        [HttpGet]
        public async Task<IActionResult> GetAllDivida()
        {
            try
            {
                var divida = await _repo.GetAllDivida();
                var result = _map.Map<IEnumerable<DividaDto>>(divida);

                return Ok(result);

            } catch(System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Lista Todos. CODE: ${e.Message}");
            }
        }
        // Lista Por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var divida = await _repo.GetDividaById(id);
                var result = _map.Map<DividaDto>(divida);

                if (result == null) return NotFound("Nenhum registro encontrado!!");

                return Ok(result);
            } catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Lista por id. CODE: ${e.Message}");
            }
        }
        // Listar Por Titulo ou Descricao do Produto ou Valor
        [HttpGet("Buscar/{buscar}")]
        public async Task<IActionResult> GetByTituloOrDescricaoOrValor(string buscar)
        {
            try
            {
                var buscarDividaPorTituloOuDescricaoProdutoOuValor = await _repo.GetDividaByTituloOrDescricaoProdutoOrValor(buscar);
                var result = _map.Map<DividaDto[]>(buscarDividaPorTituloOuDescricaoProdutoOuValor);

                return Ok(result);

            } catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Lista busca completa. CODE: ${e.Message}");
            }
        }
        // Adicionar
        [HttpPost]
        public async Task<IActionResult> Add(DividaDto dividaDto)
        {
            try
            {
                var divida = _map.Map<Divida>(dividaDto);

                _repo.Add(divida);

                if(await _repo.SaveChanges())
                {
                    return Created($"/api/divida/{divida.Id}", divida);
                }

            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Adicionar. CODE: ${e.Message}");
            }
            return BadRequest();
        }
        // Atualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DividaDto dividaDto)
        {
            try
            {
                var divida = await _repo.GetDividaById(id);
                if (divida == null) return NotFound("Nenhum registro encontrado com esse Id");

                _map.Map(dividaDto, divida);
                _repo.Update(divida);

                if(await _repo.SaveChanges())
                {
                    return Created($"api/divida/{dividaDto.Id}", _map.Map<DividaDto>(divida));
                }
            } catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Atualizar. CODE: ${e.Message}");
            }
            return BadRequest();
        }
        // Deletar
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var divida = await _repo.GetDividaById(id);
                if (divida == null) return NotFound("Nenhum registro encontrado com esse Id");

                _repo.Delete(divida);
                if (await _repo.SaveChanges())
                {
                    return Ok();
                }
            } catch(System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Deletar. CODE: ${e.Message}");
            }
            return BadRequest();
        }


    }
}
