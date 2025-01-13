using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleTaskApp.Models;
using SampleTaskApp.UnitOfWork;
using SampleTaskApp.Utilities;

namespace SampleTaskApp.Controllers.Anonymous
{
    [Route(ApiUrls.BaseAPI + "/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(AuthService authService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {  
            UserInfo user =new UserInfo { UserName=login.UserName,Password=login.Password };
            var data =await _unitOfWork.EfUserInfoRepository.GetByAuthCredentialAsync(user);
            string token = "";
            if (data != null) {
                 token = _authService.GenerateJwtToken(data);
            }
           
            return Ok(new TokenViewModel { Token = token });
        }
        [HttpPost("login-using-dapper")]
        public async Task<IActionResult> LoginUsingDapper([FromBody] LoginViewModel login)
        {
            UserInfo user = new UserInfo { UserName = login.UserName, Password = login.Password };
            var data = await _unitOfWork.DapperUserInfoRepository.GetByAuthCredentialAsync(user);
            string token = "";
            if (data != null)
            {
                token = _authService.GenerateJwtToken(data);
            }

            return Ok(new TokenViewModel { Token = token });
        }
    }
}
