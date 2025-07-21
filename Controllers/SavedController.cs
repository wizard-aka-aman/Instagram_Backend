using Humanizer;
using Instagram.Model.DTO;
using Instagram.Model.SavedRepo;
using Instagram.Model.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SavedController : Controller
    {
        private readonly ISavedRepository _savedRepository;
        public SavedController(ISavedRepository savedRepository)
        {
            _savedRepository = savedRepository;
        }

        [HttpGet("getallsaved/{username}")]
        public async Task<List<Saved>?> GetAllSaved(string username)
        {
            return await _savedRepository.GetAllSaved(username);
        }
        [HttpPost("addtosaved")]
        public async Task<bool?> AddSaved([FromBody] SavedDto dto)
        {
            return await _savedRepository.AddSaved(dto.UserName, dto.PostId);
        }
        [HttpPost("removesaved")]
        public async Task<bool?> RemoveSaved([FromBody]  SavedDto dto)
        {
            return await _savedRepository.RemoveSaved(dto.UserName, dto.PostId);
        }
        [HttpGet("issaved/{username}/{postId}")]
        public async Task<bool?> IsSaved(string username,int postId)
        {
            return await _savedRepository.IsSaved(username,postId);
        }
    }
}
