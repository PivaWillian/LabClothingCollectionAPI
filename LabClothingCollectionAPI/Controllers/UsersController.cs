using AutoMapper;
using LabClothingCollectionAPI.Models;
using LabClothingCollectionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using LabClothingCollectionAPI.Entities;

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

            Enum.TryParse(status, out Status myStatus);
            var usersCollection = await _labRepository.GetUsersAsync(myStatus);
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
        public async Task<ActionResult<UserDto>> CreateUser(UserForCreationDto user)
        {
            IEnumerable<User> users = await _labRepository.GetUsersAsync();
            var userForCreation = _mapper.Map<User>(user);
            var userCheck = users.Where(x => x.DocNumber == user.DocNumber).FirstOrDefault();
            if(userCheck != null)
            {
                return Conflict("CPF/CNPJ já cadastrado");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }

            _labRepository.CreateUser(userForCreation);
            var userToReturn = _mapper.Map<UserDto>(userForCreation);//verificar se ID Não vem sem número
            
            return CreatedAtRoute("CreateUser", new { userId = userToReturn.Id }, userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, UserForUpdateDto userForUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações repassadas inválidas");
            }
            var userEntity = await _labRepository.GetUserAsync(id);
            if(userEntity != null)
            {
                return NotFound("Número de registro inexistente");
            }

            return Ok();
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<UserDto>> UpdateUserStatus(int id, string status)
        {
            Enum.TryParse(status, out Status myStatus);
            var userEntity = await _labRepository.GetUserAsync(id);
            if (userEntity == null)
            {
                return NotFound("Usuário com esse identificador inexistente.");
            }

            //var userModel = _mapper.Map<UserDto>(userEntity);
            if(status == null)
            {
                return BadRequest("Valores repassados são inválidos.");
            }
            //var userUpdated = _mapper.Map<User>(userModel);
            userEntity.Status = myStatus;
            await _labRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
