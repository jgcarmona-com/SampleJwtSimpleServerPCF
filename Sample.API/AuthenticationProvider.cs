using JWTSimpleServer;
using JWTSimpleServer.Abstractions;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sample.API
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public Task ValidateClientAuthentication(JwtSimpleServerContext context)
        {
            if (context.UserName == "sampleapi" && context.Password == "sampliapipassword")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "SampleAPI")
                };

                context.Success(claims);
            }
            else
            {
                context.Reject("Invalid user authentication");
            }

            return Task.CompletedTask;
        }
    }
}
