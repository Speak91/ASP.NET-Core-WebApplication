using EmployeeServiceProto;
using Grpc.Net.Client;
using static EmployeeServiceProto.DictionariesService;

namespace EmployeeClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = true;

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });
            Employee employee = new Employee(channel);

            while (exit)
            {
                Console.WriteLine("Выберите необходимый пункт");
                Console.WriteLine("1. Добавить сотрудника");
                Console.WriteLine("2. Показать всех сотрудников");
                Console.WriteLine("3. Удалить сотрудника");
                Console.WriteLine("4. Завершить работу");
                var userChoose = Console.ReadLine();
                Console.WriteLine();
                switch (userChoose)
                {
                    case "1":
                        Console.WriteLine("Введите описание сотрудника");
                        var description = Console.ReadLine();
                       employee.CreateEmployeeType(description);
                        break;
                    case "2":
                        employee.GetAllEmployees();
                        break;
                    case "3":
                        Console.WriteLine("Введите id сотрудника");
                        var id = Console.ReadLine();
                        employee.DeleteEmployee(Convert.ToInt32(id));
                        break;
                    case "4":
                        exit = false;
                        Console.Clear();
                        Console.WriteLine("До свидания");
                        return;
                    default:
                        Console.WriteLine("Такого выбора нету");
                        break;
                }
            }

        }



    }
}