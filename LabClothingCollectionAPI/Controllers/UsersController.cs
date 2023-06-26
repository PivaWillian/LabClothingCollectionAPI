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

        /// <summary>
        /// Method use to get the users' list
        /// </summary>
        /// <param name="status">You can search users by status by passing a query param</param>
        /// <returns>An array of users</returns>
        /// <response code="200">Return the requested users' list</response>
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

        /// <summary>
        /// Method used to get a single user
        /// </summary>
        /// <param name="id">The user's id</param>
        /// <returns>A single user with the selected id</returns>
        /// <response code="200">Return the requested user</response>
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

        /// <summary>
        /// Create an user with given values
        /// </summary>
        /// <param name="user">An user object required for creation</param>
        /// <returns>The newly created user</returns>
        /// <response code="201">Returns the created user's data</response>
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

        /// <summary>
        /// Method used to change the user's attributes
        /// </summary>
        /// <param name="id">The id of the desired user</param>
        /// <param name="userForUpdate">An object with the new values</param>
        /// <returns>The updated user.</returns>
        /// <response code="200">Returns the user's new data values</response>
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

        /// <summary>
        /// Method used to change the user's status
        /// </summary>
        /// <param name="id">The user's id</param>
        /// <param name="status">The status value</param>
        /// <returns>The new status</returns>
        /// <response code="200">The new status of the user</response>
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
