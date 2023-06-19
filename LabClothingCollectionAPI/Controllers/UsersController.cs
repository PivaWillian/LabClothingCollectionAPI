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
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var usersCollection =  _labRepository.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(usersCollection);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserAsync(int id)
        {
            var user = await _labRepository.GetUserAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserForCreationDto user)
        {
            var userForCreation = _mapper.Map<User>(user);
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
            var userEntity = await _labRepository.GetUserAsync(id);
            if (userEntity == null)
            {
                return NotFound("Usuário com esse identificador inexistente.");
            }

            var userModel = _mapper.Map<UserDto>(userEntity);
            if(status != null)
            {
                userModel.Status = status;
            }
            var userUpdated = _mapper.Map<User>(userModel);
            await _labRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
