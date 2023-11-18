using JwtAuthServer;
using JwtAuthServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


//added a project reference of JwtAuhServer
namespace AuthWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthResponse?> Authenticate([FromBody] AuthRequest authRequest)
        {
            var authResponse = _jwtTokenHandler.GenerateJwtToken(authRequest);
            if(authResponse == null) return Unauthorized();
            //return Ok(authResponse);
            return authResponse;
        }
    }
}
