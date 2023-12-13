namespace CRUD_BAL.Common.GenericServices
{
    public interface IGenericService
    {
        Task<Response> CreateResponse(string status, string message);
        


    }
}
