using EmployeeService.Data;
using EmployeeService.Models.Dto;
using EmployeeService.Models.Requests;
using EmployeeService.Models.Requests.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeService.Services.Other
{
    public class AuthenticateService : IAuthencateService
    {
        /// <summary>
        /// Ключ для аутентификации
        /// </summary>
        public const string SecretCode = "Kjkszpj24.Pa$$w0rd";

        /// <summary>
        /// Массив наших сессий
        /// </summary>
        private readonly Dictionary<string, SessionDto> _sessions = new Dictionary<string, SessionDto>();

        /// <summary>
        /// Т.к жизненый цикл AuthenticateService это Singleton, а по правилам
        /// внутрь сервиса нельзя добавлять иные сервисы жизненый цикл у которых меньше текущего.
        /// И для обхода данного ограничения мы используем переменную типа IServiceScopeFactory:
        /// фабрика для создания динамических Scope обьектов
        /// </summary>
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AuthenticateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public SessionDto GetSession(string sessionToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Логин
        /// </summary>
        /// <param name="authenticationRequest"></param>
        /// <returns></returns>
        public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            EmployeeServiceDbContext context = scope.ServiceProvider.GetRequiredService<EmployeeServiceDbContext>();

            #region То же самое что внизу
            //Account account;
            //if (!string.IsNullOrWhiteSpace(authenticationRequest.Login))
            //{
            //    account = FindAccountByLogin(context, authenticationRequest.Login);
            //}
            //else
            //{
            //    null;
            //}
            #endregion
            //Здесь проверяем имеется ли данный логин в системе, если нет присваиваем Null
            Account account =
                !string.IsNullOrWhiteSpace(authenticationRequest.Login) ?
                FindAccountByLogin(context, authenticationRequest.Login) : null;

            //Если аккаунт Null то возвращаем сообщение что пользователь не найден
            if (account == null)
            {
                return new AuthenticationResponse
                {
                    Status = AuthencationStatus.UserNotFound
                };
            }

            //Если не Null то ищем такой пароль и если пароль не совпадает возвращаем сообщение о несоотвествии пароля 
            if (!PasswordUtils.VerifyPassword(authenticationRequest.Password, account.PasswordSalt, account.PasswordHash))
            {
                return new AuthenticationResponse
                {
                    Status = AuthencationStatus.InvalidPassword
                };
            }

            //Создаем сессию в бд
            AccountSession session = new AccountSession
            {
                //Присваиваем айди аккаунта
                AccountId = account.AccountId,
                //Создаем токен
                SessionToken = CreateSessionToken(account),
                //Дата создания сесси
                TimeCreated = DateTime.UtcNow,
                //дата последнего запрос к сессии
                TimeLastRequest = DateTime.UtcNow,
                //Сессия закрыта? да/нет
                IsClosed = false
            };

            //Добавляем в базу и сохраняем
            context.AccountSessions.Add(session);
            context.SaveChanges();

            SessionDto sessionDto = GetSessionDto(account, session);

            //осуществляем блокировку чтобы другой поток не вошёл
            lock (_sessions)
            {
                //Сохраняем сессию где ключ токен сессии и значение Сессия
                _sessions[session.SessionToken] = sessionDto;
            }

            //Возвращаем ответ 
            return new AuthenticationResponse
            {
                Status = AuthencationStatus.Success,
                Session = sessionDto
            };
        }

        /// <summary>
        /// Подготовка ответа о сессии
        /// </summary>
        /// <param name="account"></param>
        /// <param name="accountSession"></param>
        /// <returns></returns>
        private SessionDto GetSessionDto(Account account, AccountSession accountSession)
        {
            return new SessionDto
            {
                SessionId = accountSession.SessionId,
                SessionToken = accountSession.SessionToken,
                Accont = new AccountDto
                {
                    AccountId = account.AccountId,
                    EMail = account.EMail,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    SecondName = account.SecondName,
                    Locked = account.Locked
                }
            };
        }

        private string CreateSessionToken(Account account)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretCode);
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                    new Claim(ClaimTypes.Name, account.EMail)}),

                //Время жизни токена в минутах
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Вернуть Аккаунт по логину
        /// </summary>
        /// <param name="context"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        private Account FindAccountByLogin(EmployeeServiceDbContext context, string login)
        {
            return context.Accounts.FirstOrDefault(account => account.EMail == login);
        }
    }
}
