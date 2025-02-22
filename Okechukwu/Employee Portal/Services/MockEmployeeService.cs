using Employee_Portal.Models;

namespace Employee_Portal.Services
{
    public class MockEmployeeService : IEmployeeService
    {
        public async Task<Employee> GetEmployeeByIdAsync(string id)
        {
            // Simulated employee data
            return await Task.FromResult(new Employee
            {
                Id = id,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Position = "Software Developer",
                Department = "IT",
                HireDate = DateTime.Now.AddYears(-2),
                DateOfBirth = DateTime.Now.AddYears(-30)
            });
        }
    }
}