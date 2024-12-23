using AutoMapper;
using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Application.Employees.Repositories;
using EmployeeTimeApi.Application.Employees.Services;
using EmployeeTimeApi.Domain.Employees.Exceptions;
using EmployeeTimeApi.Domain.Employees.Models;
using EmployeeTimeApi.Infrastructure.Repositories;
using EmployeeTimeApi.Tests.Integration.Bases;
using Moq;

namespace EmployeeTimeApi.Tests.Integration;
public class EmployeesServiceTests : BaseIntegrationTest
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IEmployeeAccessValidationService> _validationServiceMock;
    private readonly IEmployeesRepository _repository;

    public EmployeesServiceTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
        _mockMapper = new Mock<IMapper>();
        _validationServiceMock = new Mock<IEmployeeAccessValidationService>();
        _repository = new EmployeesRepository(_connectionFactory);
    }

    [Fact]
    public async Task Add_Employee_Should_Return_Correct_Id_When_Employee_Is_Successfully_Added()
    {
        // Arrange
        var dto = new AddEmployeeDto(
            "Jan",
            "Kowalski",
            "jan.kowalski@example.com");

        var employee = new Employee(
            1,
            "jan",
            "kowalski",
            "jan.kowalski@example.com");

        _mockMapper
            .Setup(mapper => mapper.Map<Employee>(dto))
            .Returns(employee);

        var service = new EmployeesService(
            _repository,
            _mockMapper.Object,
            _validationServiceMock.Object);

        // Act
        var addedId = await service.AddAsync(dto, default);

        // Assert
        _mockMapper
            .Verify(mapper => mapper.Map<Employee>(dto),
            Times.Once);
        Assert.Equal(1, addedId);
    }

    [Fact]
    public async Task Add_Employee_Should_Throws_Exception_When_Email_Is_Taken()
    {
        // Arrange
        var dto = new AddEmployeeDto(
            "Adam",
            "Nowak",
            "adam.nowak@example.com");

        var employee = new Employee(
            5,
            "Adam",
            "Nowak",
            "adam.nowak@example.com");

        _mockMapper
            .Setup(mapper => mapper.Map<Employee>(dto))
            .Returns(employee);

        _validationServiceMock
         .Setup(vs => vs.ValidateAsync(1, default))
         .ReturnsAsync(It.IsAny<EmployeeDto>);

        var service = new EmployeesService(
            _repository,
            _mockMapper.Object,
            _validationServiceMock.Object);

        await service.AddAsync(dto, default);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EmailAlreadyTakenException>(
            () => service.AddAsync(dto, CancellationToken.None));
    }
}