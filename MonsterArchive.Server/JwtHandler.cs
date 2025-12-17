using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MonsterArchive.Server
{
    public class JwtHandler(UserManager<MonsterArchiveUser> userManager, IConfiguration configuration)
    {
        public async Task<JwtSecurityToken> GenerateTokenAsync(MonsterArchiveUser user)
        {
            return new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"]!,
                audience: configuration["JwtSettings:Audience"]!,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["JwtSettings:ExpiryInMinutes"]!)),
                claims: await GetClaimsAsync(user),
                signingCredentials: GetSigningCredentials()
            );
        }
        private SigningCredentials GetSigningCredentials()
        {
            byte[] key = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!);
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(key);
            return new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaimsAsync(MonsterArchiveUser user)
        {
            List<Claim> claims = [new Claim(ClaimTypes.Name, user.UserName!)];
            foreach (var role in await userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}