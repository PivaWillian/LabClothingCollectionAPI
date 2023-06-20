using LabClothingCollectionAPI.DbContexts;
using LabClothingCollectionAPI.Entities;
using LabClothingCollectionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LabClothingCollectionAPI.Services
{
    public class LabClothingRepository : ILabClothingRepository
    {
        private readonly LabClothingContext _context;

        public LabClothingRepository(LabClothingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #region
        public async Task<IEnumerable<User>> GetUsersAsync(Status? status)
        {
            if(status != null)
            {
                return await _context.Users.Where(c => c.Status == status).ToListAsync();
            }
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            return await _context.Users.Where(c => c.Id == userId).FirstOrDefaultAsync();
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }
        #endregion
        #region
        public async Task<IEnumerable<Model>> GetModelsAsync()
        {
            return await _context.Models.ToListAsync();
        }

        public async Task<Model?> GetModelAsync(int modelId)
        {
            return await _context.Models.Where(c => c.Id == modelId).FirstOrDefaultAsync();
        }
        #endregion
        #region
        public async Task<IEnumerable<Collection>> GetCollectionsAsync()
        {
            return await _context.Collections.ToListAsync();
        }
        public async Task<Collection?> GetCollectionAsync(int collectionId)
        {
            return await _context.Collections.Where(c => c.Id == collectionId).FirstOrDefaultAsync();
        }
        #endregion
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}
