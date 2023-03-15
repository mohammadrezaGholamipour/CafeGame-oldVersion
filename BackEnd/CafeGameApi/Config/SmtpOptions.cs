using System.Net;

namespace CafeGameApi.Config;

public record SmtpOptions
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public NetworkCredential Credential { get; set; } = null!;
    public bool EnableSsh { get; set; } = true;
    // public string Username { get; set; } = string.Empty;
    // public string Password { get; set; } = string.Empty;
}