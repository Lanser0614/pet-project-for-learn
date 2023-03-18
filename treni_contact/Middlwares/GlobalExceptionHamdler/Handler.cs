using System.Net;
using System.Text.Json;
using treni_contact.Exceptions;
using treni_contact.Exceptions.Errors;

namespace treni_contact.Middlwares.GlobalExceptionHamdler;

public class Handler : AbstractExceptionHandler
{
    public Handler(RequestDelegate next) : base(next)
    {
    }

    public override (HttpStatusCode code, string message) GetResponse(System.Exception exception)
    {
        HttpStatusCode code;
        switch (exception)
        {
            case DataBaseException:
                code = HttpStatusCode.NotFound;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                break;
        }
        return (code, JsonSerializer.Serialize(new Error(exception.Message)));
    }
}