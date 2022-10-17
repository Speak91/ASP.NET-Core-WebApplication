using EmployeeService.Data;
using EmployeeServiceProto;
using Grpc.Core;
using static EmployeeServiceProto.DepartmentService;

namespace EmployeeService.Services.Other
{
    public class DepartmentService : DepartmentServiceBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public override Task<AddDepartmentResponse> AddDepartment(AddDepartmentRequest request, ServerCallContext context)
        {
            var result = _departmentRepository.Create(new Department
            {
                Description = request.Description
            });

            return Task.FromResult(new AddDepartmentResponse { Id = result.ToString()});
        }

        public override Task<DeleteDepartmentResponse> DeleteDepartment(DeleteDepartmentRequest request, ServerCallContext context)
        {
            _departmentRepository.Delete(new Guid(request.Id));
            return Task.FromResult(new DeleteDepartmentResponse());
        }

        public override Task<GetAllDepartmentsResponse> GetAllDepartments(GetAllDepartmentsRequest request, ServerCallContext context)
        {
            GetAllDepartmentsResponse response = new GetAllDepartmentsResponse();

            response.DepartmentDto.AddRange(_departmentRepository.GetAll().Select(dep =>
                new DepartmentDto
                {
                    Id = dep.Id.ToString(),
                    Description = dep.Description,
                }
            ).ToList());

            return Task.FromResult(response);
        }

        public override Task<UpdateDepartmentResponse> UpdateDepartment(UpdateDepartmentRequest request, ServerCallContext context)
        {
            _departmentRepository.Update(new Department { Id = new Guid(request.Id), Description = request.Description });
            return Task.FromResult(new UpdateDepartmentResponse());
        }
    }
}
