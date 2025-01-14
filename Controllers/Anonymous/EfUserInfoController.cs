using Dapper;
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
            try
            {
                var data = await _unitOfWork.EfUserInfoRepository.GetAllAsync();
                var rType = new CommonOperation { Type = 6, Data = data,Total = data.Count(), Status = StatusCodes.Status200OK };
                return Ok(rType);
            }
            catch (Exception)
            {

                return Ok(new CommonOperation
                {
                    Type = 5,
                    Status = StatusCodes.Status500InternalServerError, // Return appropriate status codes for errors
                    Message = "An error occurred while fetching users data."
                });
            }

           
        }
        // GET: rk/api/userinfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospitals>> GetUserInfo(int id)
        {
            try
            {
                var data = await _unitOfWork.EfUserInfoRepository.GetByIdAsync(id);
                var rType = new CommonOperation { Type = 6, Data = data, Status = StatusCodes.Status200OK };
                return Ok(rType);
            }
            catch (Exception)
            {

                return Ok(new CommonOperation
                {
                    Type = 5,
                    Status = StatusCodes.Status500InternalServerError, // Return appropriate status codes for errors
                    Message = "An error occurred while fetching users data."
                });
            }
            
        }
        // POST:rk/api/userinfo
        [HttpPost]
        public async Task<IActionResult> CreateUserInfo([FromBody] UserInfo userInfo)
        {
           
            try
            {
                await _unitOfWork.EfUserInfoRepository.AddAsync(userInfo);
                await _unitOfWork.CompleteAsync();
                var rType = new CommonOperation { Type = 6, Data = "", Status = StatusCodes.Status200OK };
                return Ok(rType);
            }
            catch (Exception)
            {

                return Ok(new CommonOperation
                {
                    Type = 5,
                    Status = StatusCodes.Status500InternalServerError, // Return appropriate status codes for errors
                    Message = "An error occurred while creating users data."
                });
            }
        }

        //// PUT: rk/api/userinfo/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUserInfo(int id, [FromBody] UserInfo userInfo)
        //{
        //    var existingUserInfo = await _unitOfWork.EfUserInfoRepository.GetByIdAsync(id);
        //    if (existingUserInfo == null)
        //    {
        //        return NotFound();
        //    }

            
        //    existingUserInfo.UserName = userInfo.UserName;
        //    existingUserInfo.Password = userInfo.Password;

        //    await _unitOfWork.EfUserInfoRepository.UpdateAsync(existingUserInfo);
        //    await _unitOfWork.CompleteAsync();

        //    return NoContent();
        //}

        //// DELETE: rk/api/userinfo/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUserInfo(int id)
        //{
        //    await _unitOfWork.EfUserInfoRepository.DeleteAsync(id);
        //    await _unitOfWork.CompleteAsync();
        //    return NoContent();
        //}
    }

}
