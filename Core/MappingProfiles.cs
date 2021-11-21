using AutoMapper;
using VinylCollection.Entities;

namespace VinylCollection.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Vinyl, Vinyl>();
        }

    }
}