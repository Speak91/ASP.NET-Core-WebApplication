namespace EmployeeService.Models.Dto
{
    public class SessionDto
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// Токен сессии
        /// </summary>
        public string SessionToken { get; set; }

        /// <summary>
        /// Информация об аккаунте
        /// </summary>
        public AccountDto Accont { get; set; }
    }
}
