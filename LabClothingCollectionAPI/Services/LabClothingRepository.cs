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

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }
    }
}
