using CorporateTraningSystm.Model;

namespace CorporateTraningSystm.Services
{
    public interface ITrainerService
    {
        Task<IEnumerable<Trainer>> GetAllTrainersAsync();
        Task<Trainer> GetTrainerByIdAsync(int id);
        Task AddTrainerAsync(Trainer trainer);
        Task UpdateTrainerAsync(Trainer trainer);
        Task DeleteTrainerAsync(int id);
    }
}
