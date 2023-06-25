using AutoMapper;
using LabClothingCollectionAPI.Models;
using LabClothingCollectionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using LabClothingCollectionAPI.Entities;
using LabClothingCollectionAPI.Enums;

namespace LabClothingCollectionAPI.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsersController : ControllerBase
    {
        private readonly ILabClothingRepository _labRepository;
        private readonly IMapper _mapper;

        public UsersController(ILabClothingRepository labReposiroty, IMapper mapper)
        {
            _labRepository = labReposiroty ?? throw new ArgumentNullException(nameof(labReposiroty));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(string? status)
        {
            if(status == null)
            {
                var users = await _labRepository.GetUsersAsync();
                return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
            }

            var usersCollection = await _labRepository.GetUsersAsync(status);
            var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(usersCollection);
            return Ok(usersToReturn);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserAsync(int id)
        {
            var user = await _labRepository.GetUserAsync(id);
            if(user == null)
            {
                return NotFound("Identificador não corresponde há um usuário.");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody]UserForCreationDto user)
        {
            if(user.UserType != "Administrador" && user.UserType != "Gerente" 
                && user.UserType != "Criador" && user.UserType != "Outro")
            {
                return BadRequest("Tipo do usuário inválido");
            }
            if(user.Status != "Ativo" && user.Status != "Inativo")
            {
                return BadRequest("Status inválido");
            }
            IEnumerable<User> users = await _labRepository.GetUsersAsync();
            var userCheck = users.Where(x => x.DocNumber == user.DocNumber).FirstOrDefault();
            if(userCheck != null)
            {
                return Conflict("CPF/CNPJ já cadastrado");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }
            var userForCreation = _mapper.Map<User>(user);
            _labRepository.CreateUser(userForCreation);
            await _labRepository.SaveChangesAsync();
            var userToReturn = _mapper.Map<UserDto>(userForCreation);

            return Created(@"http://localhost7258/api/usuarios/", $"id = {userToReturn.Id} e tipo = {userToReturn.UserType}" );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, UserForUpdateDto userForUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações repassadas inválidas");
            }
            var userEntity = await _labRepository.GetUserAsync(id);
            if(userEntity == null)
            {
                return NotFound("Número de registro inexistente");
            }

            _mapper.Map(userForUpdate, userEntity);
            await _labRepository.SaveChangesAsync();

            return Ok($"Dados alterados {userForUpdate.FullName}, {userForUpdate.Gender}, {userForUpdate.BirthDate}, {userForUpdate.PhoneNumber}"+
                $" {userForUpdate.UserType}");
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<UserDto>> UpdateUserStatus(int id, [FromBody]string status)
        {
            var userEntity = await _labRepository.GetUserAsync(id);
            if (userEntity == null)
            {
                return NotFound("Usuário com esse identificador inexistente.");
            }

            //var userModel = _mapper.Map<UserDto>(userEntity);
            if(status == null || (status != "Ativo" && status != "Inativo"))
            {
                return BadRequest("Valores repassados são inválidos.");
            }
            //var userUpdated = _mapper.Map<User>(userModel);
            userEntity.Status = status;
            await _labRepository.SaveChangesAsync();

            return Ok($"Novo status = {status}");
        }
    }
}
