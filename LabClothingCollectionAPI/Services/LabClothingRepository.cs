using LabClothingCollectionAPI.DbContexts;
using LabClothingCollectionAPI.Entities;
using LabClothingCollectionAPI.Models;
using Microsoft.EntityFrameworkCore;
using LabClothingCollectionAPI.Enums;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string? status)
        {
            return await _context.Users.Where(c => c.Status == status).ToListAsync();
            
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

        public void CreateModel(Model model)
        {
            _context.Models.Add(model);
        }
        #endregion
        #region
        public async Task<IEnumerable<Collection>> GetCollectionsAsync()
        {
            return await _context.Collections.ToListAsync();
        }

        public async Task<IEnumerable<Collection>> GetCollectionsAsync(Status? status)
        {
            return await _context.Collections.Where(c => c.Status == status).ToListAsync();
        }
        public async Task<Collection?> GetCollectionAsync(int collectionId)
        {
            return await _context.Collections.Where(c => c.Id == collectionId).FirstOrDefaultAsync();
        }

        public void RemoveCollection(Collection collection)
        {
            _context.Collections.Remove(collection);
        }
        

        public void CreateCollection(Collection collection)
        {
            _context.Collections.Add(collection);
        }
        #endregion
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}
