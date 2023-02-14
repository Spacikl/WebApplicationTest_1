using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB2.Data;
using WEB2.Models;
using WEB2.Models.Domein;

namespace WEB2.Controllers;

public class EmployeesController : Controller
{
    
    private MVCDbContext mvcDbContext;
    
    public EmployeesController(MVCDbContext mvcDbContext)
    {
        this.mvcDbContext = mvcDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var listEployees = await mvcDbContext.Employees.ToListAsync();
        return View(listEployees);
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddEmployeeNewModel addEmployeeRequest)
    {
        
        var newEmployee = new Employee()
        {
            Id = Guid.NewGuid(),
            Name = addEmployeeRequest.Name,
            Email = addEmployeeRequest.Email,
            Department = addEmployeeRequest.Department,
            Salary = addEmployeeRequest.Salary,
            DateOfBirth = addEmployeeRequest.DateOfBirth
        };
        
        await mvcDbContext.Employees.AddAsync(newEmployee);
        await mvcDbContext.SaveChangesAsync();
        return RedirectToAction("Add");
    }

    [HttpGet]
    public async Task<IActionResult> Content(Guid id)
    {
        var employee = await mvcDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if (employee != null)
        {
            var newModel = new UpdateEmployeeModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth
            };
            return await Task.Run(() => View("Content",newModel));
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Content(UpdateEmployeeModel model)
    {
        var employee = await mvcDbContext.Employees.FindAsync(model.Id);
        if (employee != null)
        {
            employee.Name = model.Name;
            employee.Department = model.Department;
            employee.Email = model.Email;
            employee.Salary = model.Salary;
            employee.DateOfBirth = model.DateOfBirth;
            await mvcDbContext.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(UpdateEmployeeModel model)
    {
        var employee = await mvcDbContext.Employees.FindAsync(model.Id);
        if (employee != null)
        {
            mvcDbContext.Employees.Remove(employee);
            await mvcDbContext.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}