using Shared.DTO_s;
using Shared.DTO_s.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IAuthenticationService
    {


        //login    take==>Email,Password  return====>DisplayName,Email,Token
        Task<UserResultDto> LoginAsync(LoginDto loginDto);




        //Register  take==>Email,Password,PhoneNummber,UserName,DisplayName   return===>DisplayName,Email,Token
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);


        //Check Email
        Task<bool> CheckEmailAsync(string email);
     
        //Get Current User
        Task<UserResultDto> GetCurrentUserAsync(string email);

        Task<string> ForgotPasswordAsync(string Email);
         Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);



    }
}
