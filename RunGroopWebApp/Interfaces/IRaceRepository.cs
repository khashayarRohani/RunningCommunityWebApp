using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    public interface IRaceRepository
    {
       

        Task<Race?> GetByIdAsync(int id);
        Task<Race?> GetByIdAsyncNoTracking(int id);


        Task<IEnumerable<Race>> GetAll();

        Task<IEnumerable<Race>> GetAllRacesByCity(string city);

        bool Add(Race race);

        bool Update(Race race);

        bool Delete(Race race);

        bool Save();
    }
}
