﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PFA_ProjectAPI.Models.DtoAuth;

namespace PFA_ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        //POST: /apî/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName=registerRequestDto.Username,
                Email=registerRequestDto.Username

            };
            var identityResult=await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if(identityResult.Succeeded)
            {
                //Add Roles to this User
                if(registerRequestDto.Roles!=null && registerRequestDto.Roles.Any())
                {
                    identityResult= await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if(identityResult.Succeeded)
                    {

                        return Ok("User was registered! Please login.");
                    }
                }
                
            }
            return BadRequest("Something went wrong");
        }

        //POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
            var user =await userManager.FindByEmailAsync(loginRequestDto.Username);
            if(user != null)
            {
                var checkPasswordResult= await userManager.CheckPasswordAsync(user,loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Create Token 
                    return Ok();
                }
            }
            return BadRequest("Username or password incorrect");
        }


    }
}
