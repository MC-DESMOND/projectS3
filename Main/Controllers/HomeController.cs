using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.Services;
using MongoDB.Driver.Linq;

namespace Main.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly EmployeeService _empServices;

    public HomeController(ILogger <HomeController> logger, EmployeeService employeeService)
    {
        _logger = logger;
        _empServices = employeeService;
    }

    public async Task<IActionResult> Index()
    {
        await _empServices.CreateEmployee(
            new Employee{
                Name = "okechukwu",
                Email = "rggrb@gmail.com",
                Department = "sfegrmk",
                Position = "mflmfm",
                Gender = "female",
                Salary = "30,000",
                Type = EmployeeType.Permanent
            }
        );
        var employees = await _empServices.GetAllEmployees();
        return View(employees);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Index([FromQuery] string? name, [FromQuery] string? department, [FromQuery] string? type)
    {
        var employees = await _empServices.FilterEmployees(name, department, type);
        return View(employees);
    }

    [HttpGet("update")]
    public async Task<IActionResult> Update([FromQuery] string? id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid employee ID.");
        }

        var employee = await _empServices.GetEmployeeById(id);
        if (employee == null)
        {
            return NotFound("Employee not found.");
        }

        return View(employee);
    }

    [HttpGet("updateSuccess")]
    public async Task<IActionResult> UpdateSuccess([FromQuery] string? id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid employee ID.");
        }

        var employee = await _empServices.GetEmployeeById(id);
        if (employee == null)
        {
            return NotFound("Employee not found.");
        }

        return View(employee);
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
