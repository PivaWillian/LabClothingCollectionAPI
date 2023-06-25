using LabClothingCollectionAPI.Entities;
using LabClothingCollectionAPI.Enums;

namespace LabClothingCollectionAPI.Services
{
    public interface ILabClothingRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<User>> GetUsersAsync(string? status);
        Task<User?> GetUserAsync(int userId);
        void CreateUser(User user);
        Task<IEnumerable<Model>> GetModelsAsync();
        Task<Model?> GetModelAsync(int modelId);
        Task<IEnumerable<Collection>> GetCollectionsAsync();
        Task<IEnumerable<Collection>> GetCollectionsAsync(Status status);
        void CreateCollection(Collection collection);
        Task<Collection?> GetCollectionAsync(int collectionId);
        Task<bool> SaveChangesAsync();
    }
}