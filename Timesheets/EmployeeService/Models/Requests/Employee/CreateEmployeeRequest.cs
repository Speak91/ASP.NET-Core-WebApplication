namespace EmployeeService.Models.Requests.Employee
{
    public class CreateEmployeeRequest
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeTypeId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public decimal Salary { get; set; }
    }
}
