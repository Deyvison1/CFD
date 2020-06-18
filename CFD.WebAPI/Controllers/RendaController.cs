using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFD.WebAPI._services;
using CFD.WebAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CFD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RendaController : ControllerBase
    {
        private readonly RendaService _rendaService;
        public RendaController(RendaService rendaService) { _rendaService = rendaService; }

        // Listar Todos
        [HttpGet]
        public async Task<IActionResult> GetAllRenda()
        {
            try
            {
                var renda = await _rendaService.GetAllRenda();

                return Ok(renda);
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"RENDA: Erro no Lista Todos. CODE: {e.Message}");
            }
        }
        // Listar por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRendaById(int id)
        {
            try
            {
                var renda = await _rendaService.GetRendaById(id);

                return Ok(renda);
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"RENDA: Erro no Lista por id. CODE: {e.Message}");
            }
        }
        // Listar Por Titulo ou Valor ou Descricao
        [HttpGet("Buscar/{buscar}")]
        public async Task<IActionResult> GetRendaByTituloOrValorOrDescricao(string buscar)
        {
            try
            {
                var renda = await _rendaService.GetRendaByTituloOrValorOrDescricao(buscar);

                return Ok(renda);

            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"RENDA: Erro no Lista por Titulo ou Valor ou Descricao. CODE: {e.Message}");
            }
        }
        // Adicionar
        [HttpPost]
        public async Task<IActionResult> Add(RendaDto rendaDto)
        {
            try
            {
                var renda = await _rendaService.Add(rendaDto);

                return Created($"api/renda/{renda.Id}", renda);
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"RENDA: Erro no adicionar. CODE: {e.Message}");
            }
        }
        // Atualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(RendaDto rendaDto)
        {
            try
            {
                var renda = await _rendaService.Update(rendaDto);

                return Created($"api/renda/{renda.Id}", renda);
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"RENDA: Erro no atualizar. CODE: {e.Message}");
            }
        }
        // Deletar
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _rendaService.Delete(id);

                return Ok();
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"RENDA: Erro no atualizar. CODE: {e.Message}");
            }
        }
    }
}
