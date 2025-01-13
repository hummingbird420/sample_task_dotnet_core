using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleTaskApp.UnitOfWork;
using SampleTaskApp.Utilities;

namespace SampleTaskApp.Controllers.Anonymous
{
    [Route(ApiUrls.BaseAPI + "/[controller]")]
    [ApiController]
    public class EfUserInfoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EfUserInfoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET:rk/api/userinfo
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var userInfo = await _unitOfWork.EfUserInfoRepository.GetAllAsync();
            return Ok(userInfo);
        }
        // GET: rk/api/userinfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospitals>> GetUserInfo(int id)
        {
            var userInfo = await _unitOfWork.EfUserInfoRepository.GetByIdAsync(id);

            if (userInfo == null)
            {
                return NotFound();
            }

            return Ok(userInfo);
        }
        // POST:rk/api/userinfo
        [HttpPost]
        public async Task<IActionResult> CreateUserInfo([FromBody] UserInfo userInfo)
        {
            await _unitOfWork.EfUserInfoRepository.AddAsync(userInfo);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetUserInfo), new { id = userInfo.Id }, userInfo);
        }

        // PUT: rk/api/userinfo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserInfo(int id, [FromBody] UserInfo userInfo)
        {
            var existingUserInfo = await _unitOfWork.EfUserInfoRepository.GetByIdAsync(id);
            if (existingUserInfo == null)
            {
                return NotFound();
            }

            existingUserInfo.Name = userInfo.Name;
            existingUserInfo.UserName = userInfo.UserName;
            existingUserInfo.Password = userInfo.Password;

            await _unitOfWork.EfUserInfoRepository.UpdateAsync(existingUserInfo);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: rk/api/userinfo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInfo(int id)
        {
            await _unitOfWork.EfUserInfoRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }

}
