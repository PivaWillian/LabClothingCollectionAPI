using AutoMapper;

namespace LabClothingCollectionAPI.Profiles
{
    public class CollectionProfile: Profile
    {
        public CollectionProfile() 
        {
            CreateMap<Entities.Collection, Models.CollectionDto>();
            CreateMap<Models.CollectionDto, Entities.Collection>();
            CreateMap<Models.CollectionForCreationDto, Entities.Collection>();
            CreateMap<Models.CollectionForUpdateDto, Entities.Collection>();
        }
    }
}
