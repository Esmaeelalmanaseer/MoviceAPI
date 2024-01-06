using AutoMapper;

namespace MoviceAPI.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDetalisDto>();
            CreateMap<MovieDTO, Movie>()
                .ForMember(src=>src.Poster,opt=>opt.Ignore());
        }
    }
}
