namespace App1.Interfaces
{
    public interface IUserModel
    {
        DateTime? CreatedAt { get; }
        string? Email { get; set; }
        ulong? ID { get; }
        string? Password { get; set; }
        string? Username { get; set; }
    }
}