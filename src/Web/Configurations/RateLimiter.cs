namespace Web.Configurations;

public class RateLimitSettings
{
    public int GlobalLimit { get; set; }
    public int UserLimit { get; set; }
    public int EndpointLimit { get; set; }
}