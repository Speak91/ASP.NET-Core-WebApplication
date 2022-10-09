namespace EmployeeService.Models.Requests.Employee
{
    public class UpdateEmployee
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public int EmployeeTypeId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public decimal Salary { get; set; }
    }
}
