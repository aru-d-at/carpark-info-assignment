using Microsoft.AspNetCore.Mvc;

using CarParkInfo.API.DTOs;
using CarParkInfo.API.Data;
using CarParkInfo.API.Models;


namespace CarParkInfo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserFavoritesController : ControllerBase
{
    private readonly CarParkContext _context;

    public UserFavoritesController(CarParkContext context)
    {
        _context = context;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddFavorite([FromBody] UserFavoriteRequestDto favoriteRequest)
    {
        if (!_context.CarParks.Any(cp => cp.CarParkNo == favoriteRequest.CarParkNo))
        {
            return NotFound("Car park not found");
        }

        if (_context.UserFavorites.Any(uf => uf.CarParkNo == favoriteRequest.CarParkNo))
        {
            return Conflict("Car park already in favourites");
        }

        var favorite = new UserFavorite
        {
            UserId = favoriteRequest.UserId,
            CarParkNo = favoriteRequest.CarParkNo
        };

        _context.UserFavorites.Add(favorite);
        await _context.SaveChangesAsync();

        var result = new UserFavoriteResponseDto
        {
            Id = favorite.Id,
            UserId = favorite.UserId,
            CarParkNo = favorite.CarParkNo,
            AddedAt = favorite.AddedAt
        };

        return Ok(result);
    }
}