using MonsterArchive.Server.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace MonsterArchive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController (UserManager<MonsterArchiveUser> userManager, JwtHandler jwtHandler) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            MonsterArchiveUser? monsterUser = await userManager.FindByNameAsync(loginRequest.Username);
            if (monsterUser == null)
            {
                return Unauthorized("Invalid username");
            }

            bool loginStatus = await userManager.CheckPasswordAsync(monsterUser, loginRequest.Password);

            if (!loginStatus)
            {
                return Unauthorized("Invalid password");
            }
            
            JwtSecurityToken jwtToken = await jwtHandler.GenerateTokenAsync(monsterUser);
            string stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Mom loves me",
                Token = stringToken
            });
        }
    }
}
