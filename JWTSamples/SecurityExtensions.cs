using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTSamples
{
    public static class SecurityExtensions
    {
        public static User PopulateJwtSecurityToken(User user)
        {
            var signingKey = Encoding.UTF8.GetBytes("MyLongJwtSecretKey");
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature);

            var claim = new Claim(ClaimTypes.Name, user.Username);
            var claims = new Claim[] { claim };
            var claimsIdentity = new ClaimsIdentity(claims);
            var securityTokenDescriptor = new SecurityTokenDescriptor() { Subject = claimsIdentity, Expires = user.Expiry, SigningCredentials = signingCredentials, Issuer = "JWTSamples", Audience = "JWTSamples" };

            var tokenHandler = new JwtSecurityTokenHandler();
            user.SecurityToken = tokenHandler.CreateToken(securityTokenDescriptor);

            user.Token = tokenHandler.WriteToken(user.SecurityToken);

            return user;
        }
        
        public static bool ValidateUserToken(string token)
        {
            var signingKey = Encoding.UTF8.GetBytes("MyLongJwtSecretKey");
            var securityKey = new SymmetricSecurityKey(signingKey);

            var tokenValidationParams = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = securityKey
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            _ = tokenHandler.ValidateToken(token, tokenValidationParams, out SecurityToken validatedSecurityToken);
            System.Console.WriteLine(tokenHandler.WriteToken(validatedSecurityToken));

            return true;
        }
    }
}