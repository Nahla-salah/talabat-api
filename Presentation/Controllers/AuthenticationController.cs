using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DTO_s;
using Shared.DTO_s.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("Api/[controller]")]
    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {
        // POST: Api/Authentication/Login
        [HttpPost("Login")]
       
        public async Task<ActionResult<UserResultDto>> LoginAsync(LoginDto loginDto)
        {
            var result = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(result);
        }

        // POST: Api/Authentication/Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> RegisterAsync(RegisterDto registerDto)
        {
            var result = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(result);
        }

        //check Email
        [HttpGet("CheckEmail")]

        public async Task<ActionResult<bool>> CheckEmail(string email)
        {

            var result = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(result);
        }


        //Get Current User By Email
        [HttpGet("CurrentUser")]
       
        public async Task<ActionResult<UserResultDto>> GetCurrentUser(string email)
        {
            var result = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(result);
        }


        [HttpPost("Forgot-Password")]
        public async Task<ActionResult<ForgotPasswordResponseDto>> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email)) return BadRequest("Email is required.");

            
            var token = await _serviceManager.AuthenticationService.ForgotPasswordAsync(Email);

          
         
            var callbackUrl = $"https://your-frontend-domain.com/reset-password?email={Email}&token={Uri.EscapeDataString(token)}";



            return Ok(new ForgotPasswordResponseDto(Email ,token, callbackUrl));
        }

        
       
       
    
        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetDto) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _serviceManager.AuthenticationService.ResetPasswordAsync(resetDto);

            return Ok(new { Message = "Password has been reset successfully." });
        }
    }
}
