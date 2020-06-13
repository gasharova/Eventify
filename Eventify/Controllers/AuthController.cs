using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Eventify.Data.Auth;
using Eventify.DTOs.User;
using Eventify.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepo = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AddUserDTO request)
        {
            ServiceResponse<int> response = await _authRepo.Register(
                new User { Username = request.Username },
                request.Password
            );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO request)
        {
            ServiceResponse<string> response = await _authRepo.Login(
                request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<int> response = await _authRepo.Delete(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
