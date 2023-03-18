
namespace treni_contact.Http.Responses.Contact;

public class ContactCollectionResponse
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<Models.Contact.Contact> Data { get; set; }
    
    public int Count { get; set; }
    
    public int TotalPage { get; set; }
    public ContactCollectionResponse(List<Models.Contact.Contact> data, int pageNumber, int pageSize, int count)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Count = count;
        TotalPage = (int)Math.Ceiling( count / (float)pageSize);
        Data = data;
    }
}