namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;

public class RefreshTokenCommandRequestDto
{
    public string RefreshToken { get; set; }
    public string UserId { get; set; }
    public DateTime AccessTokenExpirationTime { get; set; }
    public int RefreshTokenLifeTime { get; set; } //= 30;
}
