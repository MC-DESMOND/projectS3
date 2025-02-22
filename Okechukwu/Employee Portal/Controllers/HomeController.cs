using Microsoft.AspNetCore.Mvc;
using Employee_Portal.Services;
using Employee_Portal.Models;

namespace  Employee_Portal.Controllers

{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IEmployeeService employeeService, ILogger<HomeController> logger)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Retrieves and displays employee details by ID
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>View with employee details or appropriate error response</returns>
        public async Task<IActionResult> Details(string? id)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Details action called with null or empty ID");
                return BadRequest("Employee ID is required");
            }

            try
            {
                // Fetch employee data
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                
                if (employee == null)
                {
                    _logger.LogInformation($"Employee not found with ID: {id}");
                    return NotFound($"Employee with ID {id} not found");
                }

                // Audit logging
                _logger.LogInformation($"Employee details retrieved successfully for ID: {id}");
                
                // Return view with data
                return View(employee);
            }
            catch (Exception ex)
            {
                // Structured error logging
                _logger.LogError(ex, $"Error retrieving employee details for ID: {id}");
                
                // Don't expose internal error details to client
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    "An error occurred while retrieving employee details. Please try again later."
                );
            }
        }
    }
}


// create a mock service implementation for testing purposes.