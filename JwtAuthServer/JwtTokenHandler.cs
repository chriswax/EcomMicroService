using JwtAuthServer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace JwtAuthServer
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "E47C87FF48EC4FB2ABDA514CB4B1B365";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccount> _userAccountList;

        public JwtTokenHandler()
        {
            //hardcode user account
            _userAccountList = new List<UserAccount>
            {
                new UserAccount { Username = "admin", Password = "admin123", Role = "Administrator" },
                new UserAccount { Username = "user", Password = "user123", Role = "User" },
            };
        }

        public AuthResponse? GenerateJwtToken(AuthRequest authRequest)
        {
            if(string.IsNullOrWhiteSpace(authRequest.Username) || string.IsNullOrWhiteSpace(authRequest.Password))
                return null;

            /*** Validation ***/
            var userAccount = _userAccountList.Where(x => x.Username == authRequest.Username && x.Password == authRequest.Password).FirstOrDefault();
            if(userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, authRequest.Username),
                new Claim(ClaimTypes.Role, userAccount.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokensDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokensDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthResponse
            {
                Username = userAccount.Username,
                ExpireIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token,
            };
        }

       
    }
}
