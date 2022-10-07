namespace EmployeeService.Models.Requests.Department
{
    public class UpdateDepartmentRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
