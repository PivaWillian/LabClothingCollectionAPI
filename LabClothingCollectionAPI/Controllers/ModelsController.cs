using AutoMapper;
using LabClothingCollectionAPI.Models;
using LabClothingCollectionAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabClothingCollectionAPI.Controllers
{
    public class ModelsController : ControllerBase
    {
        private readonly ILabClothingRepository _labClothing;
        private readonly IMapper _mapper;

        public ModelsController(ILabClothingRepository labClothing, IMapper mapper)
        {
            _labClothing = labClothing ?? throw new ArgumentNullException(nameof(labClothing));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("api/collection/{collectionid}/models")]
        public ActionResult<IEnumerable<ModelDto>> GetModels(int collectionId)
        {
            var models = _labClothing.GetModels(collectionId);
            return Ok(_mapper.Map<IEnumerable<ModelDto>>(models));
        }

        [HttpGet("api/collection/{collectionid/models/{id}")]
        public async Task<ActionResult<ModelDto>> GetModel(int collectionId, int id)
        {
            var collection = _labClothing.GetCollection(collectionId);
            if (collection == null)
            {
                return NotFound("Coleção informada não existe");
            }

            var model = await _labClothing.GetModelAsync(collectionId, id);
            return Ok(_mapper.Map<ModelDto>(model));
        }
    }
}
