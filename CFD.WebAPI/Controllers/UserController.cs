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
    public class UserController : ControllerBase
    {
        private readonly ICFDRepositorio _repo;
        private readonly IMapper _map;
        public UserController(ICFDRepositorio repo, IMapper mapper) { _repo = repo; _map = mapper; }

        // Lista Todos
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var user = await _repo.GetAllUser();
                var result = _map.Map<IEnumerable<UserDto>>(user);

                return Ok(result);
            } catch(System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Lista Todos. CODE: ${e.Message}");
            }
        }
        // Lista por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _repo.GetUserById(id);
                var result = _map.Map<UserDto>(user);
                if (result == null) return NotFound("Não encontrou nenhum registro");

                return Ok(result);

            } catch(System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Lista por Id. CODE: ${e.Message}");
            }
        }
        // Listar por Nome ou Email
        [HttpGet("Buscar/{buscar}")]
        public async Task<IActionResult> GetByNomeOrEmail(string buscar)
        {
            try
            {
                var buscarPorNomeOuEmail = await _repo.GetUserByNomeOrId(buscar);
                var result = _map.Map<UserDto[]>(buscarPorNomeOuEmail);

                return Ok(result);

            } catch(System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Listar por Nome ou Email. CODE: ${e.Message}");
            }
        }
        // Adicionar
        [HttpPost]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            try
            {
                var user = _map.Map<User>(userDto);
                _repo.Add(user);

                if(await _repo.SaveChanges())
                {
                    return Created($"/api/user/{user.Id}", user);
                }
            } catch(System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Adicionar. CODE: ${e.Message}");
            }
            return BadRequest();
        }
        // Atualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserDto userDto)
        {
            try
            {
                var user = await _repo.GetUserById(id);
                if (user == null) return NotFound("Não encontrado por id");

                _map.Map(userDto, user);
                _repo.Update(user);

                if(await _repo.SaveChanges())
                {
                    return Created($"/api/user/{userDto.Id}",_map.Map<UserDto>(user));
                }
            } catch(System.Exception e)
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
                var userFinal = await _repo.GetUserById(id);
                if (userFinal == null) return BadRequest();

                _repo.Delete(userFinal);
                if(await _repo.SaveChanges())
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
