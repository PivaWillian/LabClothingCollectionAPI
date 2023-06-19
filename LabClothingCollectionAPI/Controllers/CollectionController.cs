using AutoMapper;
using LabClothingCollectionAPI.Models;
using LabClothingCollectionAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabClothingCollectionAPI.Controllers
{
    public class CollectionController : ControllerBase
    {
        private readonly ILabClothingRepository _labClothing;
        private readonly IMapper _mapper;

        public CollectionController(ILabClothingRepository labClothing, IMapper mapper)
        {
            _labClothing = labClothing ?? throw new ArgumentNullException(nameof(labClothing));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("api/collection")]
        public ActionResult<IEnumerable<CollectionDto>> GetCollections()
        {
            var collections = _labClothing.GetCollections();
            return Ok(_mapper.Map<IEnumerable<CollectionDto>>(collections));
        }

        [HttpGet("api/collection/{id}")]
        public async Task<ActionResult<CollectionDto>> GetCollection(int id)
        {
            var collection = await _labClothing.GetCollectionAsync(id);
            return Ok(_mapper.Map<CollectionDto>(collection));
        }
            
    }
}
