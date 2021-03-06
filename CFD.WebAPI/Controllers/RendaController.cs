﻿using System;
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
        // Listar Por UserId
        [HttpGet("lastRendasByUser/{id}")]
        public async Task<IActionResult> GetLastRendaById(int id) 
        {
            try {
                var rendaByUserId = await _rendaService.GetUltimosRendaAddById(id);

                return Ok(rendaByUserId);

            }catch(System.Exception e){
                throw new ArgumentException($"RENDA: Erro ao listar todos por paginacao. CODE: {e.Message}");
            }
        }
        //Listar Todos por Paginacao
        [HttpGet("pages/{page}")]
        public async Task<IActionResult> GetAllRendaPage(int page) 
        {
            try {
                var rendaPage = await _rendaService.GetAllRendapPage(page);

                return Ok(rendaPage);

            }catch(System.Exception e){
                throw new ArgumentException($"RENDA: Erro ao listar todos por paginacao. CODE: {e.Message}");
            }
        }
        // Listar Todos Por Id
        [HttpGet("rendasByUser/{id}")]
        public async Task<IActionResult> GetAllRendaById(int id)
        {
            try
            {
                var renda = await _rendaService.GetAllRendaByUserId(id);

                return Ok(renda);
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"RENDA: Erro no Lista Todos. CODE: {e.Message}");
            }
        }
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
        // Listar Ultimos Adiconados
        [HttpGet("rendaLast")]
        public async Task<IActionResult> GetUltimosRendaAdd() 
        {
            try 
            {
                var lastRendas = await _rendaService.GetUltimosRendaAdd();

                return Ok(lastRendas);
            }catch(System.Exception e) 
            {
                throw new ArgumentException($"RENDA: Erro ao listar ultimos. CODE: {e.Message}");
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
