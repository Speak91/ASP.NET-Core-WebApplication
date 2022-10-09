using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeService.Data
{
    /// <summary>
    /// Рабочий
    /// </summary>
    /// 
    [Table("Employees")]
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Department))]
        public Guid DepartmentId { get; set; }

        [ForeignKey(nameof(EmployeeType))]
        public int EmployeeTypeId { get; set; }

        [Column]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Column]
        [StringLength(255)]
        public string Surname { get; set; }

        [Column]
        [StringLength(255)]
        public string Patronymic { get; set; }

        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        public virtual EmployeeType EmployeeType { get; set; }
        public virtual Department Department { get; set; }
    }
}
