namespace EmployeeTimeApi.Domain.Employees.Models;

internal class Employee
{
    private Employee()
    {
    }

    public Employee(int id, string firstName, string lastName, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
}
