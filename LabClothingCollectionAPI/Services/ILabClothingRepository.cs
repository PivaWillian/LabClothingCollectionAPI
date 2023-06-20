using LabClothingCollectionAPI.Entities;

namespace LabClothingCollectionAPI.Services
{
    public interface ILabClothingRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(Status? status);
        Task<User?> GetUserAsync(int id);
        Task<bool> SaveChangesAsync();
        void CreateUser(User user);
    }
}