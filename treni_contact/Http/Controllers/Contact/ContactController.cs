using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using treni_contact.Application.Commands.Contact;
using treni_contact.Application.Query.Contact;
using treni_contact.Http.Requests.Contact;
using treni_contact.Http.Responses.Contact;
using treni_contact.Models.Entity.User;

namespace treni_contact.Http.Controllers.Contact;

[Authorize(AuthenticationSchemes = "Bearer")]
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
        var userName = HttpContext.User.Identity?.Name;
        request.UserName = userName;
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

    [Authorize(AuthenticationSchemes = "Bearer")]
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
    public async Task<IActionResult> MassCreatePhoneForContact([FromBody] CreatePhoneForContactRequest request)
    {
        MassCreatePhoneCommand massCreatePhoneCommand = _mapper.Map<MassCreatePhoneCommand>(request);
        var result = await _mediator.Send(massCreatePhoneCommand);
        var ok = new Dictionary<string, int>()
        {
            { "is succsus cont", result }
        };
        return Ok(ok);
    }
    
    [HttpPost]
    public async Task<IActionResult> MassCreateEmailForContact([FromBody] MassCreateEmailForContactRequest request)
    {
        MassCreateEmailForContactCommand massCreateEmailForContactCommand = _mapper.Map<MassCreateEmailForContactCommand>(request);
        var result = await _mediator.Send(massCreateEmailForContactCommand);
        var ok = new Dictionary<string, int>()
        {
            { "is succsus cont", result }
        };
        return Ok(ok);
    }
    
    [HttpPost]
    public async Task<IActionResult> MassUpdateEmailForContact([FromBody] MassUpdateEmailForContactRequest request)
    {
        MassUpdateEmailForContactCommand massCreateEmailForContactCommand = _mapper.Map<MassUpdateEmailForContactCommand>(request);
        var result = await _mediator.Send(massCreateEmailForContactCommand);
        var ok = new Dictionary<string, int>()
        {
            { "is succsus cont", result }
        };
        return Ok(ok);
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteContact(long id)
    {
        var command = new DeleteContactCommand(id);
        await _mediator.Send(command);

        return Ok(HttpStatusCode.NoContent);
    }
}