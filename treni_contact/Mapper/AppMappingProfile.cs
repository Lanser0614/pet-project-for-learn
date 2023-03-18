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
        // CreateMap<CreatePhoneForContactRequest, MassCreatePhoneCommand>();
    }
    
}