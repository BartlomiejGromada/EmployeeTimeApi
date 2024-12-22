namespace EmployeeTimeApi.Shared.Abstractions.Auth;

public class AuthOptions
{
    public string IssuerSigningKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string ValidIssuer { get; set; } = string.Empty;
    public bool ValidateAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateLifetime { get; set; }
    public TimeSpan Expiry { get; set; }
    public string Challenge { get; set; } = "Bearer";
    public CookieOptions Cookie { get; set; } = new CookieOptions();

    public class CookieOptions
    {
        public bool HttpOnly { get; set; }
        public string SameSite { get; set; } = string.Empty;
        public bool Secure { get; set; }
    }
}

