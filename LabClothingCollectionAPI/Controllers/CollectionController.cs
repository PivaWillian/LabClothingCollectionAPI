//using AutoMapper;
//using LabClothingCollectionAPI.Entities;
//using LabClothingCollectionAPI.Models;
//using LabClothingCollectionAPI.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace LabClothingCollectionAPI.Controllers
//{
//    [ApiController]
//    [Route("api/colecoes")]
//    public class CollectionController : ControllerBase
//    {
//        private readonly ILabClothingRepository _labClothing;
//        private readonly IMapper _mapper;

//        public CollectionController(ILabClothingRepository labClothing, IMapper mapper)
//        {
//            _labClothing = labClothing ?? throw new ArgumentNullException(nameof(labClothing));
//            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetCollections()
//        {
//            var collections = await _labClothing.GetCollectionsAsync();
//            return Ok(_mapper.Map<IEnumerable<CollectionDto>>(collections));
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetCollections(string? status)
//        {
//            Enum.TryParse(status, out Status myStatus);
//            var collections = await _labClothing.GetCollectionsAsync(myStatus);
//            return Ok(_mapper.Map<IEnumerable<CollectionDto>>(collections));
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<CollectionDto>> GetCollection(int id)
//        {
//            var collection = await _labClothing.GetCollectionAsync(id);
//            if(collection == null)
//            {
//                return NotFound("Coleção não encontrada.");
//            }
//            return Ok(_mapper.Map<CollectionDto>(collection));
//        }

//        [HttpPost]
//        public async Task<ActionResult<CollectionForCreationDto>> CreateCollection(CollectionForCreationDto collection)
//        {
//            var collectionList = await _labClothing.GetCollectionsAsync();
//            foreach(var col in collectionList)
//            {
//                if (col.Name == collection.Name)
//                {
//                    return Conflict("Coleção com nome já existente.");
//                }
//            }
//            if(collection == null)
//            {
//                return BadRequest("Dados inválidos");
//            }
//            var collectionToCreate = _mapper.Map<Collection>(collection);
//            _labClothing.CreateCollection(collectionToCreate);
//            return StatusCode(201, "Coleção criada com sucesso.");
//        }

//        [HttpPost]
//    }
//}
