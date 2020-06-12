using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CFD.Dominio;
using CFD.Repositorio;
using CFD.WebAPI._services;
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
        //private readonly ICFDRepositorio _repo;
        //private readonly IMapper _map;
        private readonly DividaService _dividaService;
        public DividaController(DividaService dividaService)
        {
            //_repo = repo; 
            //_map = mapper;
            _dividaService = dividaService;
        }

        // Lista Todos
        [HttpGet]
        public async Task<IActionResult> GetAllDivida()
        {
            try
            {
                var divida = await _dividaService.GetAllDivida();

                return Ok(divida);

            }
            catch (System.Exception e)
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
                var divida = await _dividaService.GetDividaById(id);

                if (divida == null) return NotFound("Nenhum registro encontrado!!");

                return Ok(divida);
            }
            catch (System.Exception e)
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
                var buscarPor = await _dividaService.GetDividaByTituloOrDescricaoProdutoOrValor(buscar);

                return Ok(buscarPor);

            }
            catch (System.Exception e)
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
                var result = await _dividaService.Add(dividaDto);
                
                return Created($"/api/divida/{result.Id}", result);
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Adicionar. CODE: ${e.Message}");
            }
        }
        // Atualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(DividaDto dividaDto)
        {
            try
            {

                await _dividaService.Update(dividaDto);

                return Created($"api/divida/{dividaDto.Id}", dividaDto);
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Atualizar. CODE: ${e.Message}");
            }
        }
        // Deletar
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var divida = await _dividaService.GetDividaById(id);
                if (divida == null) return NotFound("Nenhum registro encontrado com esse Id");

                await _dividaService.Delete(id);
                
                return Ok();
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Deletar. CODE: ${e.Message}");
            }
        }


    }
}
