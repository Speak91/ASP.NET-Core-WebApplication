using EmployeeService.Models.Dto;
using EmployeeService.Models.Requests.Authentication;

namespace EmployeeService.Services
{
    public interface IAuthencateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);

        public SessionDto GetSession(string sessionToken);
    }
}
