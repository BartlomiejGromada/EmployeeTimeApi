using AutoMapper;
using Moq;
using EmployeeTimeApi.Application.Employees.Services;
using EmployeeTimeApi.Application.Employees.Repositories;
using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Domain.Employees.Exceptions;
using EmployeeTimeApi.Domain.Employees.Models;

namespace EmployeeTimeApi.Tests.Unit;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeesRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IEmployeeAccessValidationService> _validationService;
    private readonly IEmployeesService _employeeService;

    public EmployeeServiceTests()
    {
        _repositoryMock = new Mock<IEmployeesRepository>();
        _mapperMock = new Mock<IMapper>();
        _validationService = new Mock<IEmployeeAccessValidationService>();
        _employeeService = new EmployeesService(
            _repositoryMock.Object,
            _mapperMock.Object,
            _validationService.Object);
    }

    [Fact]
    public async Task AddAsync_Should_AddEmployee_When_EmailNotTaken()
    {
        // Arrange
        var dto = new AddEmployeeDto("jan", "kowalski", "jan.kowalski@example.com");

        var employee = new Employee(
            1,
            dto.FirstName,
            dto.LastName,
            dto.Email);

        var expectedId = 1;

        _repositoryMock
            .Setup(repo => repo.IsEmailAlreadyTakenAsync(
                dto.Email,
                null,
                default))
            .ReturnsAsync(false);

        _mapperMock
            .Setup(mapper => mapper.Map<Employee>(dto))
            .Returns(employee);

        _repositoryMock
            .Setup(repo => repo.AddAsync(employee, default))
            .ReturnsAsync(expectedId);

        // Act
        var result = await _employeeService.AddAsync(dto);

        // Assert
        Assert.Equal(expectedId, result);

        _repositoryMock.Verify(repo => repo.IsEmailAlreadyTakenAsync(
           dto.Email,
           null,
           default),
           Times.Once);

        _repositoryMock.Verify(repo => repo.AddAsync(
            It.IsAny<Employee>(),
            default),
            Times.Once);

        _mapperMock.Verify(mapper => mapper.Map<Employee>(dto), Times.Once);
    }

    [Fact]
    public async Task AddAsync_Should_ThrowEmailAlreadyTakenException_When_EmailIsTaken()
    {
        // Arrange
        var dto = new AddEmployeeDto("jan", "kowalski", "jan.kowalski@example.com");

        _repositoryMock
            .Setup(repo => repo.IsEmailAlreadyTakenAsync(
                dto.Email,
                null,
                default))
            .ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<EmailAlreadyTakenException>(async () =>
            await _employeeService.AddAsync(dto));

        _repositoryMock.Verify(repo => repo.IsEmailAlreadyTakenAsync(
            dto.Email,
            null,
            default),
            Times.Once);

        _repositoryMock.Verify(repo => repo.AddAsync(
            It.IsAny<Employee>(),
            default),
            Times.Never);

        _mapperMock.Verify(mapper => mapper.Map<Employee>(
            It.IsAny<AddEmployeeDto>()),
            Times.Never);
    }
}
