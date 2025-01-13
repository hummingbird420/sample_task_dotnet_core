using Microsoft.AspNetCore.Mvc;
using SampleTaskApp.Repositories;
using SampleTaskApp.UnitOfWork;
using SampleTaskApp.Utilities;

namespace SampleTaskApp.Controllers
{
    [Route(ApiUrls.BaseAPI + "/[controller]")]
    [ApiController]
    public class DapperUserInfoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        // Constructor that takes IUnitOfWork as dependency
        public DapperUserInfoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: rk/api/DapperUserInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfos()
        {
            var userInfos = await _unitOfWork.DapperUserInfoRepository.DapperGetAllAsync();
            return Ok(userInfos);
        }

        // GET: rk/api/DapperUserInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfo>> GetUserInfo(int id)
        {
            var userInfo = await _unitOfWork.DapperUserInfoRepository.DapperGetByIdAsync(id);
            if (userInfo == null)
            {
                return NotFound();
            }
            return Ok(userInfo);
        }

        // POST: rk/api/DapperUserInfo
        [HttpPost]
        public async Task<ActionResult<UserInfo>> PostUserInfo(UserInfo userInfo)
        {
            await _unitOfWork.DapperUserInfoRepository.DapperAddAsync(userInfo);
            return CreatedAtAction("GetUserInfo", new { id = userInfo.Id }, userInfo);
        }

        // PUT: rk/api/DapperUserInfo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInfo(int id, UserInfo userInfo)
        {
            if (id != userInfo.Id)
            {
                return BadRequest();
            }

            await _unitOfWork.DapperUserInfoRepository.DapperUpdateAsync(userInfo);
            return NoContent();
        }

        // DELETE: rk/api/DapperUserInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInfo(int id)
        {
            var userInfo = await _unitOfWork.DapperUserInfoRepository.DapperGetByIdAsync(id);
            if (userInfo == null)
            {
                return NotFound();
            }

            await _unitOfWork.DapperUserInfoRepository.DapperDeleteAsync(id);
            return NoContent();
        }
    }

}
