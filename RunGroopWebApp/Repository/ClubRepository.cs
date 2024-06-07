using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;
        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Add(club);
            
            return Save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }

        public Task<List<City>> GetAllCitiesByState(string state)
        {
            throw new NotImplementedException();
        }

        public Task<List<State>> GetAllStates()
        {
            throw new NotImplementedException();
        }

        public async Task<Club?> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(a=>a.Address).FirstOrDefaultAsync(c => c.Id == id);   
        }

        public async Task<Club?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.Include(a=> a.Address).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id); 
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(x=> x.Address.City.Contains(city)).ToListAsync();
        }

        public Task<IEnumerable<Club>> GetClubsByCategoryAndSliceAsync(ClubCategory category, int offset, int size)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Club>> GetClubsByState(string state)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountByCategoryAsync(ClubCategory category)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Club>> GetSliceAsync(int offset, int size)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
            return saved>0?true: false;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}
