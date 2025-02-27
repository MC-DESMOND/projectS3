using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.Services;
namespace Main.Controllers;

public class HomeController(ILogger<HomeController> logger, EmployeeService employeeService) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly EmployeeService EmpServices = employeeService;

//Dummy Delete
[HttpGet("delete")]
public async Task<IActionResult> Delete([FromQuery] string id)
{
    
    var employee = await EmpServices.GetEmployeeById(id);
    return View(employee);
}

    [HttpGet("deleted")]
public async Task<IActionResult> Deleted([FromQuery] string id)
{
    // Dummy employee data (same as above)
    await EmpServices.DeleteEmployee(id); // Keep the delete operation

    return View();
}

    // public  async Task<IActionResult> Deleted([FromQuery] string  id)
    // {
    //     var employee = await EmpServices.GetEmployeeById(id);
    //     await EmpServices.DeleteEmployee(id);
    //     return View(employee); // Pass user details to Delete.cshtml
    // }

    public async void createDummyEmpWithName(string name, string Gender = "Male") {
            await EmpServices.CreateEmployee(
         new Employee
        {
            Name = name,
            Email = name.ToLower().Replace(" ", "")+"@example.com",
            Position = "Software Developer",
            Department = "IT",
            DateOfBirth = new DateTime(990, 5, 15).ToString("yyyy-MM-dd"),
            Type = EmployeeType.Permanent,
            Salary = "50000",
            Gender = Gender
        });}
    public async Task<IActionResult> Index()
    {
        // createDummyEmpWithName("Anita Doew","Female");
        var employees = await EmpServices.GetAllEmployees();
        return View(employees);
    }
    [HttpGet("filter")]
    public async Task<IActionResult> Index( [FromQuery] string? name, [FromQuery] string? department, [FromQuery] string? type)
    {
        var employees = await EmpServices.FilterEmployees(name, department,type);
        return View(employees);
    }
    [HttpGet("details")]
    public async Task<IActionResult> Details( [FromQuery] string? id)
    {
        var employees = await EmpServices.GetEmployeeById(id);
        return View(employees);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
