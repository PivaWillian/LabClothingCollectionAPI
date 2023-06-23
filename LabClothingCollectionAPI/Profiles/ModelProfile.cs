using AutoMapper;

namespace LabClothingCollectionAPI.Profiles
{
    public class ModelProfile: Profile
    {
        public ModelProfile() 
        {
            CreateMap<Entities.Model, Models.ModelDto>();
            CreateMap<Models.ModelDto, Entities.Model>();
            CreateMap<Models.ModelForCreationDto, Entities.Model>();
        }
    }
}
