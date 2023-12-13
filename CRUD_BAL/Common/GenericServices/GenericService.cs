namespace CRUD_BAL.Common.GenericServices
{
    public class GenericService : IGenericService
    { 
        public async Task<Response> CreateResponse(string status, string message)
        {
            return new Response { Status = status, Message = message };
        }
    }
}
