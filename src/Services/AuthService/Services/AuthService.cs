using AuthService.DTOs;
using AuthService.Entities;
using AuthService.Helpers;
using AuthService.Interfaces;
using BCrypt.Net;

namespace AuthService.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IAuthRepository repository,
        JwtTokenGenerator jwtTokenGenerator)
    {
        _repository = repository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    // Register User
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        // Check if user already exists
        var existingUser = await _repository.GetUserByEmailAsync(request.Email);

        if (existingUser != null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Email already exists."
            };
        }

        // Create new user
        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "Customer"
        };

        await _repository.AddUserAsync(user);
        await _repository.SaveChangesAsync();

        return new AuthResponse
        {
            Success = true,
            Message = "Registration successful."
        };
    }

    // Login User
    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        // Check if user exists
        var user = await _repository.GetUserByEmailAsync(request.Email);

        if (user == null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        // Verify password
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!isPasswordValid)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        // Generate JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthResponse
        {
            Success = true,
            Message = "Login successful.",
            Token = token
        };
    }
}