namespace CarParkInfo.API.DTOs;

public class UserFavoriteRequestDto
{
    public int? UserId { get; set; }
    public string? CarParkNo { get; set; }
}

public class UserFavoriteResponseDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string? CarParkNo { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}