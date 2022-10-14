using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Data
{
    [Table("AccountSessions")]
    public class AccountSession
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        /// <summary>
        /// Сгенерированный токен
        /// </summary>
        [Required]
        [StringLength(384)]
        public string SessionToken { get; set; }

        /// <summary>
        /// Указатель на текущего пользователя
        /// </summary>
        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }

        /// <summary>
        /// Дата генерации токена
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime TimeCreated { get; set; }

        /// <summary>
        /// Дата последнего запроса к системе
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime TimeLastRequest { get; set; }

        /// <summary>
        /// Идентификатор закрытия сессии
        /// </summary>
        public bool IsClosed { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? TimeClosed { get; set; }

        /// <summary>
        /// Аккаунт сессии
        /// </summary>
        public virtual Account? Account { get; set; }
    }
}
