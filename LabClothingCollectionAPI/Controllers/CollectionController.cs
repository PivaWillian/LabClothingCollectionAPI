using AutoMapper;
using LabClothingCollectionAPI.Entities;
using LabClothingCollectionAPI.Enums;
using LabClothingCollectionAPI.Models;
using LabClothingCollectionAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabClothingCollectionAPI.Controllers
{
    [ApiController]
    [Route("api/colecoes")]
    public class CollectionController : ControllerBase
    {
        private readonly ILabClothingRepository _labClothing;
        private readonly IMapper _mapper;

        public CollectionController(ILabClothingRepository labClothing, IMapper mapper)
        {
            _labClothing = labClothing ?? throw new ArgumentNullException(nameof(labClothing));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all collections or all the collections with the same status
        /// </summary>
        /// <param name="status">The status desired</param>
        /// <returns>A set of collections</returns>
        /// <response code="200">Returns the asked set of collections</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetCollections(Status? status)
        {
            if(status == null)
            {
                var collectionsToReturn = await _labClothing.GetCollectionsAsync();
                return Ok(_mapper.Map<IEnumerable<CollectionDto>>(collectionsToReturn));
            }
            var collections = await _labClothing.GetCollectionsAsync(status);
            return Ok(_mapper.Map<IEnumerable<CollectionDto>>(collections));
        }

        /// <summary>
        /// Method used to get a single collection
        /// </summary>
        /// <param name="id">The id of the collection</param>
        /// <returns>The desired collection</returns>
        /// <response code="200">The desired collection</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionDto>> GetCollection(int id)
        {
            var collection = await _labClothing.GetCollectionAsync(id);
            if (collection == null)
            {
                return NotFound("Coleção não encontrada.");
            }
            return Ok(_mapper.Map<CollectionDto>(collection));
        }

        /// <summary>
        /// Method used to create a collection
        /// </summary>
        /// <param name="collection">A set of values needed to create a collection</param>
        /// <returns>The newly created collection</returns>
        /// <response code="201">The new collection</response>
        [HttpPost]
        public async Task<ActionResult<CollectionForCreationDto>> CreateCollection(CollectionForCreationDto collection)
        {
            var collectionList = await _labClothing.GetCollectionsAsync();
            foreach (var col in collectionList)
            {
                if (col.Name == collection.Name)
                {
                    return Conflict("Coleção com nome já existente.");
                }
            }
            if (collection == null || !ModelState.IsValid)
            {
                return BadRequest("Dados inválidos");
            }

            var collectionToCreate = _mapper.Map<Collection>(collection);
            _labClothing.CreateCollection(collectionToCreate);
            await _labClothing.SaveChangesAsync();
            return Created(@"http://localhost7258/api/colecoes", collectionToCreate);
        }

        /// <summary>
        /// Method used to change a collection attributes
        /// </summary>
        /// <param name="id">The id of the collection</param>
        /// <param name="collectionForUpdate">The new values of the collection</param>
        /// <returns>The changed collection</returns>
        /// <response code="200">The changed collection</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<CollectionDto>> UpdateCollection(int id, CollectionForUpdateDto collectionForUpdate)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }
            var collectionEntity = await _labClothing.GetCollectionAsync(id);
            if(collectionEntity == null)
            {
                return NotFound("Número de registro inexistente.");
            }

            _mapper.Map(collectionForUpdate, collectionEntity);
            await _labClothing.SaveChangesAsync();

            return Ok(collectionEntity);
        }
        
        /// <summary>
        ///Method used to change the status of a collection 
        /// </summary>
        /// <param name="id">The id of the collection</param>
        /// <param name="status">The new status</param>
        /// <returns>The changed status</returns>
        /// <response code="200">The new status</response>

        [HttpPut("{id}/status")]
        public async Task<ActionResult<UserDto>> UpdateCollectinStatus(int id, [FromBody]Status status)
        {
            var collectionEntity = await _labClothing.GetCollectionAsync(id);
            if(collectionEntity == null) 
            {
                return NotFound("~Identificador inexistente para coleção");
            }
            if(status == null ||(status != Status.Ativo && status != Status.Inativo))
            {
                return BadRequest("Valores repassados são inválidos");
            }

            collectionEntity.Status = status;
            await _labClothing.SaveChangesAsync();

            return Ok($"Novo status = {collectionEntity.Status}");
        }

        /// <summary>
        /// Method used to delete a collection. All models related will also be deleted
        /// </summary>
        /// <param name="id">The id of the collection</param>
        /// <returns>No content</returns>
        /// <response code="204">No content in case of success</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCollection(int id)
        {
            var collectionEntity = await _labClothing.GetCollectionAsync(id);
            if(collectionEntity == null)
            {
                return NotFound("Coleção inexistente.");
            }
            _labClothing.RemoveCollection(collectionEntity);
            await _labClothing.SaveChangesAsync();
            return NoContent();
        }
    }
}
