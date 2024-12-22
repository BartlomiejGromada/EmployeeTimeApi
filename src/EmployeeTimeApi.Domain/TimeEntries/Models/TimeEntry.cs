namespace EmployeeTimeApi.Domain.TimeEntries.Models;

internal class TimeEntry
{
    private TimeEntry()
    {
    }
    public TimeEntry(int id, DateTime date, int hoursWorked)
    {
        Id = id;
        Date = date;
        HoursWorked = hoursWorked;
    }

    public int Id { get; private set; }
    public DateTime Date { get; private set; }
    public int HoursWorked { get; private set; }
}
