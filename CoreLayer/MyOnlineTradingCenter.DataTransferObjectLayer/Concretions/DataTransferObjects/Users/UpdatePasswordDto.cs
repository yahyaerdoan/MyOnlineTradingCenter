namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users;

public class UpdatePasswordDto
{
    public string UserId { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string ResetToken { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
}
