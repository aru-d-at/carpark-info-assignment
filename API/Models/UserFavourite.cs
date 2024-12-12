using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;


namespace CarParkInfo.API.Models;

public class UserFavorite
{
    [Key]
    [SwaggerSchema(WriteOnly = true)]
    public int Id { get; set; }
    // ForeignKey for when users are impemented
    public int? UserId { get; set; }
    public string? CarParkNo { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}