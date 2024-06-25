using CorporateTraningSystm.Data;
using CorporateTraningSystm.Model;
using Microsoft.EntityFrameworkCore;

namespace CorporateTraningSystm.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly CorporateTrainingContext _context;

        public TrainerService(CorporateTrainingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trainer>> GetAllTrainersAsync()
        {
            return await _context.Trainers.ToListAsync();
        }

        public async Task<Trainer> GetTrainerByIdAsync(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }

        public async Task AddTrainerAsync(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTrainerAsync(Trainer trainer)
        {
            _context.Entry(trainer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTrainerAsync(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
