namespace EmployeeTimeApi.Domain.Accounts.Models;

internal class Account
{
    private Account()
    {
    }

    public Account(
        int id,
        string email,
        string hashedPassword,
        string role)
    {
        Id = id;
        Email = email;
        HashedPassword = hashedPassword;
        Role = role;
    }

    public int Id { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string HashedPassword { get; set; }
    public string Role { get; set; }
}
