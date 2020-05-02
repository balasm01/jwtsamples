using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTSamples
{
    class Program
    {
        static void Main(string[] args)
        {

            var user = new User() { Username = "balasm01", Created = DateTime.Now, Expiry = DateTime.Now.AddMinutes(20) };
            user = SecurityExtensions.PopulateJwtSecurityToken(user);
            
            Console.WriteLine(user.SecurityToken.AsJsonString());
            Console.WriteLine(user.Token);
            Console.WriteLine(SecurityExtensions.ValidateUserToken(user.Token));
            Console.Read();
        }


    }
}
