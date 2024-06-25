using AutoMapper;
using CorporateTraningSystm.Model;
using CorporateTraningSystm.Services;
using CorporateTraningSystm.View;
using Microsoft.AspNetCore.Mvc;

namespace CorporateTraningSystm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerApiController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly IMapper _mapper;

        public TrainerApiController(ITrainerService trainerService, IMapper mapper)
        {
            _trainerService = trainerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrainers()
        {
            var trainers = await _trainerService.GetAllTrainersAsync();
            var trainerViewModels = _mapper.Map<IEnumerable<TrainerView>>(trainers);
            return Ok(trainerViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainerById(int id)
        {
            var trainer = await _trainerService.GetTrainerByIdAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            var trainerViewModel = _mapper.Map<TrainerView>(trainer);
            return Ok(trainerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrainer(TrainerView trainerViewModel)
        {
            if (ModelState.IsValid)
            {
                var trainer = _mapper.Map<Trainer>(trainerViewModel);
                await _trainerService.AddTrainerAsync(trainer);
                return CreatedAtAction(nameof(GetTrainerById), new { id = trainer.TrainerId }, trainer);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainer(int id, TrainerView trainerViewModel)
        {
            if (id != trainerViewModel.TrainerId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var trainer = _mapper.Map<Trainer>(trainerViewModel);
                await _trainerService.UpdateTrainerAsync(trainer);
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            await _trainerService.DeleteTrainerAsync(id);
            return NoContent();
        }
    }
}
