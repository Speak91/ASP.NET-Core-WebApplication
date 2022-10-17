using EmployeeServiceProto;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static EmployeeServiceProto.DictionariesService;

namespace EmployeeClient
{
    public class Employee
    {
        private DictionariesServiceClient _client;
        private bool exit = true;

        public Employee(GrpcChannel grpcChannel)
        {
           _client = new DictionariesServiceClient(grpcChannel);
        }

        public void CreateEmployeeType(string description)
        {
            var response = _client.CreateEmployeeType(new CreateEmployeeTypeRequest { Descrription = description });

            if (response != null)
            {
                Console.WriteLine($"Сотрудник успешно добавлен Id: {response.Id}");
            }
        }

        public void GetAllEmployees()
        {
            var getAllClients = _client.GetAllEmployeeTypes(new GetAllEmployeeTypesRequest());
            foreach (var item in getAllClients.EmployeeTypes)
            {
                Console.WriteLine($"Id: {item.Id} / Description: {item.Description}");
            }
        }

        public void DeleteEmployee(int id)
        {
            _client.DeleteEmployeeType(new DeleteEmployeeTypeRequest { Id = id});
            Console.WriteLine("Успешно удален");
        }
    }
}
