using EmployeeTimeApi.Domain.TimeEntries.Exceptions;

namespace EmployeeTimeApi.Domain.TimeEntries.Models;

internal class TimeEntry
{
    private TimeEntry()
    {
    }
    public TimeEntry(int id, int employeeId, DateTime date, int hoursWorked)
    {
        if (hoursWorked < 1 || hoursWorked > 24)
        {
            throw new InvalidHoursWorkedException();
        }

        Id = id;
        EmployeeId = employeeId;
        Date = date;
        HoursWorked = hoursWorked;
    }

    public int Id { get; private set; }
    public int EmployeeId { get; private set; }
    public DateTime Date { get; private set; }
    public int HoursWorked { get; private set; }
}
