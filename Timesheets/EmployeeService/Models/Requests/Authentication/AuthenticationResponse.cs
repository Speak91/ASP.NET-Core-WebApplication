using EmployeeService.Models.Dto;

namespace EmployeeService.Models.Requests.Authentication
{
    public class AuthenticationResponse
    {
        public AuthencationStatus Status { get; set; }
        public SessionDto Session { get; set; }
    }
}
