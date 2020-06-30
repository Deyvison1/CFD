using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CFD.Dominio;
using CFD.Repositorio;
using CFD.WebAPI._services;
using CFD.WebAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CFD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly ICFDRepositorio _repo;
        //private readonly IMapper _map;
        private readonly UserService _userService;
        private readonly ICFDRepositorio _repo;
        private readonly IMapper _map;
        public UserController(UserService userService, ICFDRepositorio repo, IMapper map)
        {
            _repo = repo;
            _map = map;
            _userService = userService;
        }
        // Listar Ultimos Usuarios.
        [HttpGet("userLast")]
        public async Task<IActionResult> GetUltimosUserAdd()
        {
            try 
            {
                var user = await _userService.GetUltimosUser();

                return Ok(user);
            }catch(System.Exception e)
            {
                throw new ArgumentException($"USER: Erro listar ultimos usuarios. CODE: {e.Message}");
            }
        }
        // Listar Quantidade de Paginas
        [HttpGet("pages")]
        public async Task<IActionResult> QuantidadeDePages() 
        {
            try {
                var qtdPages = await _userService.GetUserQtdPages();

                return Ok(qtdPages);
            }catch(System.Exception e) 
            {
                throw new ArgumentException($"USER: Erro ao listar quantidade de paginas. CODE: {e.Message}");
            }
        }
        // Listar Todos por Paginacao
        [HttpGet("page/{page}")]
        public async Task<IActionResult> GetAllUserPaginacao(int page) {
            try {
                
                var user = await _userService.GetAllUserPaginacao(page);

                return Ok(user);
            }catch(System.Exception e) {
                throw new ArgumentException($"USER: Erro ao Listar todos por Paginacao. CODE: {e.Message}");
            }
        }
        // Lista Todos
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var user = await _userService.GetAllUser();
                
                return Ok(user);
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"USER: Erro no Lista Todos. CODE: {e.Message}");
            }
        }
        // Listar por Id com Rendas e Dividas
        [HttpGet("detalhes/{id}")]
        public async Task<IActionResult> GetUserByIdAndDividasAndRendas(int id)
        {
            try
            {
                var result = await _userService.GetUserByIdAndDividasAndRendas(id);

                return Ok(result);
            }
            catch (System.Exception e)
            {
                throw new ArgumentException($"USER: Erro ao listar por Id com Rendas e Dividas. CODE: {e.Message}");
            }
        }
        // Lista por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _userService.GetUserById(id);
                return Ok(result);

            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"USER: Erro no Lista por Id. CODE: {e.Message}");
            }
        }
        // Listar por Nome ou Email
        [HttpGet("Buscar/{buscar}")]
        public async Task<IActionResult> GetByNomeOrEmail(string buscar)
        {
            try
            {
                var buscarPor = await _userService.GetUserByNomeOrIdOrPapel(buscar);

                return Ok(buscarPor);

            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"USER: Erro no Listar por Nome ou Email. CODE: {e.Message}");
            }
        }
        // LOGIN
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var check = await _userService.Login(userLoginDto);

                return Ok(
                    new
                    {
                        token = _userService.GeradorDeToken(check).Result
                    }
                );
            }
            catch (System.Exception e)
            {
                throw new ArgumentException($"USER: Erro ao logar. CODE: {e.Message}");
            }
        }
        // Adicionar
        [HttpPost]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            try
            {

                var result = await _userService.Add(userDto);

                return Created($"/api/user/{result.Id}", result);


            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"USER: Erro no Adicionar. CODE: {e.Message}");
            }
        }
        // Atualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UserDto userDto)
        {
            try
            {
                await _userService.Update(userDto);

                return Created($"/api/user/{userDto.Id}", userDto);


            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"USER: Erro no Atualizar. CODE: {e.Message}");
            }
        }
        // Deletar
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _userService.Delete(id);

                return Ok();

            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"USER: Erro no Deletar. CODE: {e.Message}");
            }
        }
    }
}
