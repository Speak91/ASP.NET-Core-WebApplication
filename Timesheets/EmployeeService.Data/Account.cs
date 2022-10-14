using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Data
{
    [Table("Accounts")]
    public class Account
    {
        /// <summary>
        /// Идентификатор аккаунта
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        [StringLength(255)]
        public string EMail { get; set; }

        /// <summary>
        /// Соль
        /// </summary>
        [StringLength(100)]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        [StringLength(100)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Признак блокировки
        /// </summary>
        public bool Locked { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string SecondName { get; set; }

        /// <summary>
        /// Сессии аккаунтов (Чтобы выкинуть пользователя)
        /// </summary>
        [InverseProperty(nameof(AccountSession.Account))]
        public virtual ICollection<AccountSession> Sessions { get; set; } = new HashSet<AccountSession>();
    }
}
