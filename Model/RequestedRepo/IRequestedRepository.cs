using Instagram.Model.DTO;
using Instagram.Model.Tables;

namespace Instagram.Model.RequestedRepo
{
    public interface IRequestedRepository
    {
        Task<bool> AddRequest(RequestDto dto);
        Task<List<Requested>> GetAllRequest(string LoogedInUser);
        bool IsRequest(string UserNameOfReqFrom, string UserNameOfReqTo);
        Task<bool> DeleteRequest(string UserNameOfReqFrom, string UserNameOfReqTo);
    }
}
