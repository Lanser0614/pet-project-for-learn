using AutoMapper;
using treni_contact.Application.Commands.Contact;
using treni_contact.Application.Query.Contact;
using treni_contact.Http.Requests.Contact;

namespace treni_contact.Mapper;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<ContactCreateRequest, CreateContactCommand>();
        CreateMap<ContactUpdateRequest, UpdateContactCommand>();
        CreateMap<ContactCollectionRequest, GetAllContactQuery>();
        CreateMap<GetOneContactRequest, GetOneContactQuery>();
        CreateMap<CreatePhoneForContactRequest, MassCreatePhoneCommand>()
            .ForMember(dest => dest.Phones,
                opt
                    => opt.MapFrom(src => src.Phones))
            .ForMember(dest => dest.ContactId,
                opt
                    => opt.MapFrom(src => src.ContactId));
        
        
        CreateMap<MassCreateEmailForContactRequest, MassCreateEmailForContactCommand>()
            .ForMember(dest => dest.Emails,
                opt
                    => opt.MapFrom(src => src.Emails))
            .ForMember(dest => dest.ContactId,
                opt
                    => opt.MapFrom(src => src.ContactId));
        
        
        CreateMap<MassUpdateEmailForContactRequest, MassUpdateEmailForContactCommand>()
            .ForMember(dest => dest.Email,
                opt
                    => opt.MapFrom(src => src.Emails))
            .ForMember(dest => dest.ContactId,
                opt
                    => opt.MapFrom(src => src.ContactId));
    }
}