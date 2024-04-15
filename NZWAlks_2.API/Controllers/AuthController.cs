using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NZWAlks_2.API.Models.DTO;
using NZWAlks_2.API.Repositories;

namespace NZWAlks_2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            //Validate the Incoming Request using Fluent Validation

            //Check if User is Authenticated (Check Username and Password)
            var user = await userRepository.AuthenticateAsync(loginRequestDTO.Username, loginRequestDTO.Password);
            if (user != null)
            {
                //Generate JWT Token
                var token = tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Username and Password is incorrect");
        }
    }
}
