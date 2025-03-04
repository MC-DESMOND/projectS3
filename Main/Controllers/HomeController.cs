using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.Services;
namespace Main.Controllers;

public class HomeController(ILogger<HomeController> logger, EmployeeService employeeService) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly EmployeeService EmpServices = employeeService;

    public async Task<IActionResult> Index()
    {
        
        var employees = await EmpServices.GetAllEmployees();
        return View(employees);
    }
    [HttpGet("filter")]
    public async Task<IActionResult> Index( [FromQuery] string? name, [FromQuery] string? department, [FromQuery] string? type)
    {
        var employees = await EmpServices.FilterEmployees(name, department,type);
        return View(employees);
    }

    [HttpGet("createform")]
    public IActionResult Create(){
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(string fullName, string email, string department, string position, string hireDate, string dateOfBirth, string employeeType, string gender, string salary)
    {
        DateTime parsedHireDate = DateTime.Parse(hireDate);
        DateTime parsedDateOfBirth = DateTime.Parse(dateOfBirth);
        decimal parsedSalary = decimal.Parse(salary);
        var employee = new Employee{
            Name = fullName,
            Email = email,
            Department = department,
            Position = position,
            HireDate = hireDate,
            DateOfBirth = dateOfBirth,
            Salary = salary,
            Gender = gender,
            Type = EmpServices.ReturnEmployeeType(employeeType),
        };
         await EmpServices.CreateEmployee(employee);
        return RedirectToAction("success", new{
            fullName = fullName,
            email = email,
            department = department,
            position = position,
            hireDate = hireDate,
            dateOfBirth = dateOfBirth,
            employeeType = employeeType,
            gender = gender,
            salary = salary
        });}
    [HttpGet("success")]
    public IActionResult CreateSuccess(string fullName, string email, string department, string position, string hireDate, string dateOfBirth, string employeeType, string gender, string salary)
    {
        ViewBag.FullName = fullName;
        ViewBag.Email = email;
        ViewBag.Department = department;
        ViewBag.Position = position;
        ViewBag.HireDate = hireDate;
        ViewBag.DateOfBirth = dateOfBirth;
        ViewBag.EmployeeType = employeeType;
        ViewBag.Gender = gender;

        string cleanedSalary = salary.Replace("$", "").Replace(",", "").Trim();
        if (decimal.TryParse(cleanedSalary, out decimal salaryValue))
        {
            ViewBag.Salary = salaryValue.ToString("C");
        }
        else
        {
            ViewBag.Salary = "$0.00";
        }

        return View();
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
