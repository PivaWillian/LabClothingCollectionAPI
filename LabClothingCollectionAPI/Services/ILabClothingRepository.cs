using LabClothingCollectionAPI.Entities;

namespace LabClothingCollectionAPI.Services
{
    public interface ILabClothingRepository
    {
        Task<User?> GetUserAsync(int id);
        IEnumerable<User> GetUsers();
        Task<bool> SaveChangesAsync();
        void CreateUser(User user);
    }
}