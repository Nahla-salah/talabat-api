using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction;
using Shared.DTO_s;
using Shared.DTO_s.IdentityModule;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplementation
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager ,IConfiguration _configuration) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            //Check if the user exists ===>Email
          var User = await  _userManager.FindByEmailAsync(loginDto.Email);
            if (User == null)
            {
                throw new UserNotFoundException(loginDto.Email);    
            }


            //Check if the password is correct
            var isPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (!isPasswordValid)
            {
                throw new UnAuthorizedException();
            }
     
                return new UserResultDto(  User.DisplayName, User.Email, await CreateTokenAsync(User));

          
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            //Create a new user
            var User =new ApplicationUser
           { 
           DisplayName = registerDto.DisplayName,
              Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
           };
          var Result =  await _userManager.CreateAsync(User, registerDto.Password);
            if (!Result.Succeeded)
            {
                var errors = string.Join(" ,", Result.Errors.Select(e => e.Description));
                throw new Exception($"User registration failed: {errors}");
            }
            return new UserResultDto(User.DisplayName, User.Email, await CreateTokenAsync(User));
        }


        //Create Token
        public async Task<String> CreateTokenAsync(ApplicationUser user) 
        {
            //Create claims
            var Claims = new List<Claim>()
            {
            new (ClaimTypes.Name, user.UserName),
            new (ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier ,user.Id)
            };

            //Add roles claims
            var Roles =await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //Create the token
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWTOptions")["Issuer"],
                audience: _configuration.GetSection("JWTOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credintials);

            return new JwtSecurityTokenHandler().WriteToken(Token);





        }
    
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user =await _userManager.FindByEmailAsync(email);
            return user != null;
        }

   

        public async Task<UserResultDto> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException(email);
            }
            return new UserResultDto(user.DisplayName, user.Email, await CreateTokenAsync(user));
        }

        public async Task<string> ForgotPasswordAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                throw new UserNotFoundException(Email);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }
      
     

      
       



        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                throw new UserNotFoundException(resetPasswordDto.Email);
            }

            var result = await _userManager.ResetPasswordAsync(
                user,
                resetPasswordDto.Token,
                resetPasswordDto.NewPassword
            );

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Reset password failed: {errors}");
            }

            return true;
        }

    }
    }
   

