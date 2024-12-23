using EmployeeTimeApi.Domain.TimeEntries.Exceptions;
using EmployeeTimeApi.Domain.TimeEntries.Models;

namespace EmployeeTimeApi.Tests.Unit;

public class TimeEntryTests
{

    [Fact]
    public void Constructor_Should_Throw_InvalidHoursWorkedException_For_Invalid_Hours()
    {
        // Arrange
        var invalidHours = -5;


        // Act & Assert
        Assert.Throws<InvalidHoursWorkedException>(() =>
          new TimeEntry(
              id: 1,
              employeeId: 1,
              date: DateTime.Now,
              hoursWorked: invalidHours));
    }

    [Fact]
    public void Constructor_Should__Object_For_Valid_Hours()
    {
        // Arrange
        var validHours = 8;

        // Act
        var timeEntry = new TimeEntry(
            id: 1,
            employeeId: 1,
            date: DateTime.Now,
            hoursWorked: validHours);

        // Assert
        Assert.NotNull(timeEntry);
        Assert.Equal(validHours, timeEntry.HoursWorked);
    }
}
