using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using treni_contact.Application.Commands.Contact;
using treni_contact.Application.Query.Contact;
using treni_contact.Http.Requests.Contact;
using treni_contact.Http.Responses.Contact;

namespace treni_contact.Http.Controllers.Contact;

[ApiController]
[Route("api/[controller]/[action]")]
public class ContactController : ControllerBase
{
    // private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ContactController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
        // _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> StoreContacts([FromBody] ContactCreateRequest request)
    {
        /*
         * без аutoMaper
         */
        // var contact = await _mediator.Send(new CreateContactCommand(
        //     request.FirstName,
        //     request.SecondName
        //     ));

        // return Ok(contact);

        /*
        * с аutoMaper
        */
        CreateContactCommand createContactCommand = _mapper.Map<CreateContactCommand>(request);

        var contact = await _mediator.Send(createContactCommand);

        return Ok(contact);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContact([FromQuery] ContactCollectionRequest filter)
    {
        GetAllContactQuery getAllContactCommand = _mapper.Map<GetAllContactQuery>(filter);
        var result = await _mediator.Send(getAllContactCommand);

        var data = new ContactCollectionResponse(result.Contacts, result.PageNumber, result.PageSize, result.Count);

        return Ok(data);
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactUpdateRequest request)
    {
        request.Id = id;

        UpdateContactCommand updateContactCommand = _mapper.Map<UpdateContactCommand>(request);
        var result = await _mediator.Send(updateContactCommand);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetContact([FromQuery] GetOneContactRequest request)
    {
        GetOneContactQuery getOneContactQuery = _mapper.Map<GetOneContactQuery>(request);
        var result = await _mediator.Send(getOneContactQuery);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePhoneForContact([FromBody] CreatePhoneForContactRequest request)
    {
        var massCreatePhoneCommand = new MassCreatePhoneCommand(
            request.ContactId, request.Phones
        );
        var result = await _mediator.Send(massCreatePhoneCommand);
        var ok = new Dictionary<string, int>()
        {
            { "is succsus cont", result }
        };
        return Ok(ok);
    }
}