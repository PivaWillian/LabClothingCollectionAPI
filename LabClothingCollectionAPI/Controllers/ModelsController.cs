using AutoMapper;
using LabClothingCollectionAPI.Entities;
using LabClothingCollectionAPI.Enums;
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
        public async Task<ActionResult<IEnumerable<ModelDto>>> GetModels(Layout? layout)
        {
            if(layout == null)
            {
                var modelsToReturn = await _labClothing.GetModelsAsync();
                return Ok(_mapper.Map<IEnumerable<ModelDto>>(modelsToReturn));
            }
            var models = await _labClothing.GetModelsAsync(layout);
            return Ok(_mapper.Map<IEnumerable<ModelDto>>(models));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModelDto?>> GetModel(int id)
        {
            var model = await _labClothing.GetModelAsync(id);
            if(model == null)
            {
                return NotFound("Não encontrado.");
            }
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
            foreach(var modelo in modelsForComparison)
            {
                if (modelo.Name == modelForCreation.Name)
                {
                    return Conflict("Nome de modelo já existente");
                }
            }
            var model = _mapper.Map<Model>(modelForCreation);
            _labClothing.CreateModel(model);
            await _labClothing.SaveChangesAsync();
            var modelToReturn = _mapper.Map<ModelDto>(model);

            return Created(@"http://localhost7258/api/modelos", modelToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ModelDto>> UpdateModel(int id, ModelForUpdateDto modelForUpdate)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Dados informados são inválidos.");
            }
            var modelEntity = await _labClothing.GetModelAsync(id);
            if (modelEntity == null)
            {
                return NotFound("Modelo não encontrado.");
            }

            _mapper.Map(modelForUpdate, modelEntity);
            await _labClothing.SaveChangesAsync();

            return Ok(modelEntity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModel(int id)
        {
            var modelToDelete = await _labClothing.GetModelAsync(id);
            if(modelToDelete == null)
            {
                return NotFound("Não encontrado modelo.");
            }
            _labClothing.RemoveModel(modelToDelete);
            await _labClothing.SaveChangesAsync();
            return NoContent();
        }
    }
}
