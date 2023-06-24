using AutoMapper;
using LabClothingCollectionAPI;

namespace LabClothingCollectionAPI.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile() 
        {
            CreateMap<Entities.User, Models.UserDto>();
            CreateMap<Models.UserDto, Entities.User>();
            CreateMap<Models.UserForCreationDto, Entities.User>();
            CreateMap<Entities.User, Models.UserForCreationDto>();
        }
    }
}
