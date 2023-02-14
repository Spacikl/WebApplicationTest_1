using WEB2.Models.Domein;

namespace WEB2.Models;

public class AddEmployeeNewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Salary { get; set; }
    public string Department { get; set; }
    public DateTime DateOfBirth { get; set; }
}