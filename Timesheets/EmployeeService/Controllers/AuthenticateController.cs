using EmployeeService.Models.Dto;
using EmployeeService.Models.Requests.Authentication;
using EmployeeService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace EmployeeService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthencateService _authencateService;
        public AuthenticateController(IAuthencateService authencateService)
        {
            _authencateService = authencateService;
        }

        /// <summary>
        /// Логинимся
        /// </summary>
        /// <param name="authenticationRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        public IActionResult Login([FromBody] AuthenticationRequest authenticationRequest)
        {
            AuthenticationResponse authenticationResponse = _authencateService.Login(authenticationRequest);
            if (authenticationResponse.Status == AuthencationStatus.Success)
            {
                Response.Headers.Add("X-Session-Token", authenticationResponse.Session.SessionToken);
            }
            return Ok(authenticationResponse);
        }

        [HttpGet]
        [Route("session")]
        [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
        public IActionResult GetSession()
        {
            var authorizationHeader = Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(authorizationHeader, out var headerValue))
            {
                var scheme = headerValue.Scheme; //Значение схемы авторизации
                var sessionToken = headerValue.Parameter; //Значение токена

                if (string.IsNullOrEmpty(sessionToken))
                    return Unauthorized();

                SessionDto sessionDto = _authencateService.GetSession(sessionToken);
                if (sessionDto == null)
                    return Unauthorized();

                return Ok(sessionDto);
            }

            return Unauthorized();
        }
    }
}
