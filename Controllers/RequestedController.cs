using Instagram.Model.NotificationRepo;
using Instagram.Model;
using Microsoft.AspNetCore.Mvc;
using Instagram.Model.DTO;
using Instagram.Model.RequestedRepo;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestedController : Controller
    {
        private readonly InstagramContext _societyContext;
        private readonly IRequestedRepository _requestedRepository;
        public RequestedController(InstagramContext societyContext, IRequestedRepository requestedRepository)
        {
            _societyContext = societyContext;
            _requestedRepository = requestedRepository;
        }


        [HttpGet]
        [Route("getAllRequested/{username}")]
        public async Task<IActionResult> GetFollowers(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be null or empty.");
            }
            var followers = await _requestedRepository.GetAllRequest(username);

            return Ok(followers);
        }
        [HttpGet]
        [Route("isRequested/{UserNameOfReqFrom}/{UserNameOfReqTo}")]
        public IActionResult IsRequested(string UserNameOfReqFrom, string UserNameOfReqTo)
        {
            if (string.IsNullOrEmpty(UserNameOfReqFrom))
            {
                return BadRequest("Username cannot be null or empty.");
            }
            var followers = _requestedRepository.IsRequest(UserNameOfReqFrom, UserNameOfReqTo);

            return Ok(followers);
        }
        [HttpDelete]
        [Route("DeleteRequest/{UserNameOfReqFrom}/{UserNameOfReqTo}")]
        public async Task<IActionResult> DeleteRequest(string UserNameOfReqFrom, string UserNameOfReqTo)
        {
            if (string.IsNullOrEmpty(UserNameOfReqFrom))
            {
                return BadRequest("Username cannot be null or empty.");
            }
            var followers = await _requestedRepository.DeleteRequest(UserNameOfReqFrom, UserNameOfReqTo);

            return Ok(followers);
        }
        [HttpPost]
        [Route("AddRequested")]
        public async Task<IActionResult> AddRequested([FromBody] RequestDto dto)
        {
            if (string.IsNullOrEmpty(dto.UserNameOfReqFrom))
            {
                return BadRequest("Username cannot be null or empty.");
            }
            var followers = await _requestedRepository.AddRequest(dto);

            return Ok(followers);
        }
    }
}
