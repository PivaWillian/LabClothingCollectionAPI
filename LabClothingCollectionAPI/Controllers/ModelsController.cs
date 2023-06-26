using AutoMapper;
using LabClothingCollectionAPI.Entities;
using LabClothingCollectionAPI.Models;
using LabClothingCollectionAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabClothingCollectionAPI.Controllers
{
    [ApiController]
    [Route("api/modelos")]
    public class ModelsController : ControllerBase
    {
        private readonly ILabClothingRepository _labClothing;
        private readonly IMapper _mapper;

        public ModelsController(ILabClothingRepository labClothing, IMapper mapper)
        {
            _labClothing = labClothing ?? throw new ArgumentNullException(nameof(labClothing));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelDto>>> GetModels()
        {
            var models = await _labClothing.GetModelsAsync();
            return Ok(_mapper.Map<IEnumerable<ModelDto>>(models));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModelDto?>> GetModel(int modelId)
        {
            var model = await _labClothing.GetModelAsync(modelId);
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<ModelDto>> CreateModel(ModelForCreationDto modelForCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados informados inválidos");
            }
            var modelsForComparison = await _labClothing.GetModelsAsync();
            foreach(var model in modelsForComparison)
            {
                if (model.Name == modelForCreation.Name)
                {
                    return Conflict("Nome de modelo já existente");
                }
            }
            var model = _mapper.Map<Model>(modelForCreation);
            _labClothing.CreateModel(model);
            await _labClothing.SaveChangesAsync();
            var modelToReturn = _mapper.Map<ModelDto>(model);

            return Created(@"http://localhost7258/api/modelos", );
        }
    }
}
