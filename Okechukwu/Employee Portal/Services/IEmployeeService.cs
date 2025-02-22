using Employee_Portal.Models;

namespace Employee_Portal.Services
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeByIdAsync(string id);
    }
}