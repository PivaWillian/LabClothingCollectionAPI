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
