using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scavdue.Business.Models.Request;
using Scavdue.Business.Models.Response;
using IAuthorizationService = Scavdue.Business.Interfaces.IAuthorizationService;

namespace Scavdue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public AuthorizationController(IMapper mapper, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestModel model)
        {
            var response = await _authorizationService.AuthenticateAsync(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshRequestModel refreshRequestModel)
        {
            var refreshToken = refreshRequestModel.RefreshToken;
            var response = await _authorizationService.RefreshTokenAsync(refreshToken);

            if (response == null)
                return Forbid();

            return Ok(response);
        }
    }
}
