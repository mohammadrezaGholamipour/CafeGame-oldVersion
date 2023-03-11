namespace CafeGameApi.ConfigModels;

public record JwtOptions
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public long DurationInMinutes { get; set; }
}