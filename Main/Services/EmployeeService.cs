using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Main.Settings;
using Main.Models;

namespace Main.Services{

public class EmployeeService
{
    private readonly IMongoCollection<Employee> _employees;

    public EmployeeService(MongoDBSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _employees = database.GetCollection<Employee>(settings.EmployeesCollectionName);
    }

    public async Task UpdateEmployee(string id, Employee employee) =>
        await _employees.ReplaceOneAsync(emp => emp.Id == id, employee);

    public EmployeeType ReturnEmployeeType(string type){
        if (type.Equals("contract", StringComparison.CurrentCultureIgnoreCase))
            {
            return EmployeeType.Contract;
        }else if (type.Equals("permanent", StringComparison.CurrentCultureIgnoreCase))
            {
            return EmployeeType.Permanent;
        }else if (type.Equals("temporary", StringComparison.CurrentCultureIgnoreCase))
            {
            return EmployeeType.Temporary;
        }else{
            return EmployeeType.Temporary;
        }
    }
    public async Task<List<Employee>> GetAllEmployees() =>
        await _employees.Find(emp => true).ToListAsync();

    public async Task<Employee> GetEmployeeById(string id) =>
        await _employees.Find(emp => emp.Id == id).FirstOrDefaultAsync();

    public async Task CreateEmployee(Employee employee) =>
        await _employees.InsertOneAsync(employee);



     public async Task<List<Employee>> FilterEmployees(string? name, string? department,string? type)
        {
            var filter = Builders<Employee>.Filter.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                filter &= Builders<Employee>.Filter.Eq("name", name);
            }

            if (!string.IsNullOrEmpty(department))
            {
                filter &= Builders<Employee>.Filter.Eq("department", department);
            }
            if (!string.IsNullOrEmpty(type))
            {
                filter &= Builders<Employee>.Filter.Eq("type", type);
            }


            return await _employees.Find(filter).ToListAsync();
        }
    
}
}