using AutoMapper;
namespace RealWorld.Features;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.User, Users.User>(MemberList.None);
    }
}
