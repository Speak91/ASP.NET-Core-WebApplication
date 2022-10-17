using EmployeeService.Models.Dto;
using EmployeeServiceProto;
using Grpc.Core;
using static EmployeeServiceProto.DictionariesService;

namespace EmployeeService.Services.Other
{
    public class DictionariesService : DictionariesServiceBase
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository;

        public DictionariesService(IEmployeeTypeRepository employeeTypeRepository)
        {
            _employeeTypeRepository = employeeTypeRepository;
        }

        public override Task<CreateEmployeeTypeResponse> CreateEmployeeType(CreateEmployeeTypeRequest request, ServerCallContext context)
        {
            var result = _employeeTypeRepository.Create(new Data.EmployeeType
            {
                Description = request.Description
            });

            CreateEmployeeTypeResponse response = new CreateEmployeeTypeResponse();
            response.Id = result;
            return Task.FromResult(response);
        }

        public override Task<DeletedEmployeeTypeResponse> DeletedEmployeeType(DeletedEmployeeTypeRequest request, ServerCallContext context)
        {
            _employeeTypeRepository.Delete(request.Id);
            return Task.FromResult(new DeletedEmployeeTypeResponse());
        }

        public override Task<GetAllEmployeeTypesResponse> GetAllEmployeeTypes(GetAllEmployeeTypesRequest request, ServerCallContext context)
        {
            GetAllEmployeeTypesResponse response = new GetAllEmployeeTypesResponse();
            response.EmployeeTypes.AddRange(_employeeTypeRepository.GetAll().Select(employeeType => new EmployeeServiceProto.EmployeeType
            {
                Id = employeeType.Id,
                Description = employeeType.Description,
            }));

            return Task.FromResult(response);
        }

        public override Task<GetEmployeeTypeByIdResponse> GetEmployeeTypeById(GetEmployeeTypeByIdRequest request, ServerCallContext context)
        {
            GetEmployeeTypeByIdResponse response = new GetEmployeeTypeByIdResponse();
            var responseFromDb = _employeeTypeRepository.GetById(request.Id);

            response.Description = responseFromDb.Description;
            response.Id = responseFromDb.Id;

            return Task.FromResult(response);
        }

        public override Task<UpdateEmployeeTypeResponse> UpdateEmployeeType(UpdateEmployeeTypeRequest request, ServerCallContext context)
        {
            _employeeTypeRepository.Update(new Data.EmployeeType { Description = request.Description, Id = request.Id });
            return Task.FromResult(new UpdateEmployeeTypeResponse());
        }
    }
}
