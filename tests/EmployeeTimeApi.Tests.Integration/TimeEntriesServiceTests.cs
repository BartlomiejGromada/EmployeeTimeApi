using AutoMapper;
using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Application.Employees.Repositories;
using EmployeeTimeApi.Application.Employees.Services;
using EmployeeTimeApi.Application.TimeEntries.ApiObjects;
using EmployeeTimeApi.Application.TimeEntries.Dtos;
using EmployeeTimeApi.Application.TimeEntries.Repositories;
using EmployeeTimeApi.Application.TimeEntries.Services;
using EmployeeTimeApi.Domain.Employees.Models;
using EmployeeTimeApi.Domain.TimeEntries.Exceptions;
using EmployeeTimeApi.Domain.TimeEntries.Models;
using EmployeeTimeApi.Infrastructure.Repositories;
using EmployeeTimeApi.Tests.Integration.Bases;
using Moq;

namespace EmployeeTimeApi.Tests.Integration;
public class TimeEntriesServiceTests : BaseIntegrationTest
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IEmployeeAccessValidationService> _validationServiceMock;

    private readonly ITimeEntriesRepository _repository;
    private readonly IEmployeesRepository _employeeRepository;

    public TimeEntriesServiceTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
        _mockMapper = new Mock<IMapper>();
        _validationServiceMock = new Mock<IEmployeeAccessValidationService>();
        _repository = new TimeEntriesRepository(_connectionFactory);
        _employeeRepository = new EmployeesRepository(_connectionFactory);
    }

    [Fact]
    public async Task Add_Time_Entry_Should_End_Successfully()
    {
        // Arrange
        var employeeId = 1;
        var date = DateTime.Now;
        var hoursWorked = 8;

        var dto = new AddTimeEntryDto(
            date,
            hoursWorked);

        await _employeeRepository.AddAsync(new Employee(
            1,
            "Jan",
            "Kowalski",
            "jan.kowalski@example.com"), default);

        _mockMapper
            .Setup(mapper => mapper.Map<TimeEntry>(dto))
            .Returns(new TimeEntry(1, employeeId, date, hoursWorked));

        _validationServiceMock
          .Setup(vs => vs.ValidateAsync(employeeId, default))
          .ReturnsAsync(new EmployeeDto(
              employeeId,
              "Jan",
              "Kowalski",
              "jan.kowalski@example.com"));

        var service = new TimeEntriesService(
            _repository,
            _mockMapper.Object,
            _validationServiceMock.Object);

        // Act
        await service.AddAsync(employeeId, dto, default);

        var timeEntriesPaged = await service.GetPagedAsync(employeeId, new BrowseTimeEntriesQuery
        {
            Page = 1,
            Results = 1
        });

        // Assert
        _mockMapper
            .Verify(mapper => mapper.Map<TimeEntry>(dto),
            Times.Once);
        Assert.Single(timeEntriesPaged.Items);
    }

    [Fact]
    public async Task Add_Time_Entry_Should_Throw_Exception_When_Invalid_Hours_Worked()
    {
        // Arrange
        var employeeId = 1;
        var date = DateTime.Now;
        var hoursWorked = -1;

        var dto = new AddTimeEntryDto(
            date,
            hoursWorked);

        await _employeeRepository.AddAsync(new Employee(
            1,
            "Adam",
            "Nowak",
            "adam.nowak@example.com"), default);

        _mockMapper
            .Setup(mapper => mapper.Map<TimeEntry>(dto))
            .Returns(new TimeEntry(1, employeeId, date, 5));

        _validationServiceMock
          .Setup(vs => vs.ValidateAsync(employeeId, default))
          .ReturnsAsync(new EmployeeDto(
              employeeId,
              string.Empty,
              string.Empty,
              string.Empty));

        var service = new TimeEntriesService(
            _repository,
            _mockMapper.Object,
            _validationServiceMock.Object);

        // Act &  Assert
        var exception = await Assert.ThrowsAsync<InvalidHoursWorkedException>(
            () => service.AddAsync(employeeId, dto, default));
    }
}