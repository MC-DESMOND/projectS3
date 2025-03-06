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
        return RedirectToAction("CreateSuccess", new{
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
    [HttpGet]
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
    [HttpGet("updateform")]
    public async Task<IActionResult> Update(string id){
        var employee = await EmpServices.GetEmployeeById(id);
        return View(employee);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(string id,string fullName, string email, string department, string position, string hireDate, string dateOfBirth, string employeeType, string gender, string salary)
    {
        DateTime parsedHireDate = DateTime.Parse(hireDate);
        DateTime parsedDateOfBirth = DateTime.Parse(dateOfBirth);
        decimal parsedSalary = decimal.Parse(salary);
        var employee = new Employee{
            Id = id,
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
         await EmpServices.UpdateEmployee(id,employee);
        return RedirectToAction("UpdateSuccess", new{
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
    [HttpGet]
    public IActionResult UpdateSuccess(string fullName, string email, string department, string position, string hireDate, string dateOfBirth, string employeeType, string gender, string salary)
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
