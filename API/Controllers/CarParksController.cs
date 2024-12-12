using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CarParkInfo.API.Models;
using CarParkInfo.API.Data;
using CarParkInfo.API.DTOs;

namespace CarParkInfo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarParksController : ControllerBase
{
    private readonly CarParkContext _context;

    public CarParksController(CarParkContext context)
    {
        _context = context;
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterCarParks(
        [FromQuery] bool detailed = false,
        [FromQuery] string? freeParking = null,
        [FromQuery] string? nightParking = null,
        [FromQuery] double? vehicleHeight = null
    )
    {
        var carParks = _context.CarParks.AsQueryable();

        if (detailed)
        {
            // Eager load ParkingDetail if 'detailed' is true
            carParks = carParks.Include(cp => cp.ParkingDetail);
        }

        if (!string.IsNullOrEmpty(freeParking))
            carParks = carParks.Where(cp => EF.Functions.Like(
                cp.ParkingDetail.FreeParking, $"%{freeParking}%")
            );

        if (!string.IsNullOrEmpty(nightParking))
            carParks = carParks.Where(cp => EF.Functions.Like(
                cp.ParkingDetail.NightParking, $"%{nightParking}%")
            );

        if (vehicleHeight.HasValue)
            carParks = carParks.Where(cp => cp.GantryHeight >= vehicleHeight);
        
        var result = await carParks.ToListAsync();

        var DTOList = result.Select<CarPark, object>(cp =>
            detailed
                ? new CarParkDetailedDto
                {
                    CarParkNo = cp.CarParkNo,
                    Address = cp.Address,
                    XCoord = cp.XCoord,
                    YCoord = cp.YCoord,
                    CarParkType = cp.CarParkType,
                    TypeOfParkingSystem = cp.TypeOfParkingSystem,
                    CarParkDecks = cp.CarParkDecks,
                    GantryHeight = cp.GantryHeight,
                    CarParkBasement = cp.CarParkBasement,
                    ParkingDetail = new ParkingDetailDto
                    {
                        ShortTermParking = cp.ParkingDetail.ShortTermParking,
                        FreeParking = cp.ParkingDetail.FreeParking,
                        NightParking = cp.ParkingDetail.NightParking
                    }
                }
                : new CarParkDto
                {
                    CarParkNo = cp.CarParkNo,
                    Address = cp.Address,
                    XCoord = cp.XCoord,
                    YCoord = cp.YCoord,
                    CarParkType = cp.CarParkType,
                    TypeOfParkingSystem = cp.TypeOfParkingSystem,
                    CarParkDecks = cp.CarParkDecks,
                    GantryHeight = cp.GantryHeight,
                    CarParkBasement = cp.CarParkBasement,
                }).ToList();

        return Ok(DTOList);
    }
}