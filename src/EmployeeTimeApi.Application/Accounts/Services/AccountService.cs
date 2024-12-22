
using AutoMapper;
using EmployeeTimeApi.Application.Accounts.Dtos;
using EmployeeTimeApi.Application.Accounts.Repositories;
using EmployeeTimeApi.Domain.Accounts.Exceptions;
using EmployeeTimeApi.Domain.Accounts.Models;
using EmployeeTimeApi.Shared.Abstractions.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeTimeApi.Application.Accounts.Services;

internal sealed class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;
    private readonly IPasswordHasher<Account> _passwordHasher;
    private readonly IMapper _mapper;
    private readonly AuthOptions _authOptions;
    private readonly ILogger<AccountService> _logger;

    public AccountService(
        IAccountRepository repository,
        IPasswordHasher<Account> passwordHasher,
        IMapper mapper,
        AuthOptions authOptions,
        ILogger<AccountService> logger)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _authOptions = authOptions;
        _logger = logger;
    }

    public async Task RegisterAsync(
        RegisterDto dto,
        CancellationToken? cancellationToken = null)
    {
        var isEmailAlreadyTaken = await _repository.IsExistAsync(dto.Email, cancellationToken);

        if (isEmailAlreadyTaken)
        {
            throw new AccountAlreadyExistException(dto.Email);
        }

        var account = _mapper.Map<Account>(dto);
        account.HashedPassword = _passwordHasher.HashPassword(account, dto.Password);
        account.Role = dto.IsAdmin ? Roles.Admin : Roles.User;

        await _repository.AddAsync(account);
    }

    public async Task<string> GenerateTokenAsync(
        LoginDto dto,
        CancellationToken? cancellationToken = null)
    {
        var account = await _repository.GetByEmailAsync(dto.Email, cancellationToken);

        if (account is null)
        {
            throw new AccountDoesntExistException(dto.Email);
        }

        var result = _passwordHasher.VerifyHashedPassword(account, account.HashedPassword, dto.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new InvalidEmailOrPasswordException();
        }

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new(ClaimTypes.Email, account.Email),
            new(ClaimTypes.Role, account.Role),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.IssuerSigningKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expireDate = DateTime.Now.Add(_authOptions.Expiry);

        var token = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            audience: _authOptions.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expireDate,
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public void Signout(int accountId)
    {
        _logger.LogInformation($"Account with id: {accountId} logged out");
    }
}
