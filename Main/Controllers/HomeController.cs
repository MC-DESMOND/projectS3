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
