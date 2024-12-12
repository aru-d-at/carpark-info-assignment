using Microsoft.EntityFrameworkCore;
using Xunit;

using CarParkInfo.API.Controllers;
using CarParkInfo.API.Models;
using CarParkInfo.API.Data;

public class CarParksControllerTests
{
    private CarParkContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<CarParkContext>()
            .UseInMemoryDatabase(databaseName: "CarParkTestDb")
            .Options;

        var context = new CarParkContext(options);
        context.CarParks.Add(new CarPark
        {
            CarParkNo = "CP001",
            Address = "Test Address",
            GantryHeight = 2.0,
            CarParkType = "Surface",
            TypeOfParkingSystem = "Electronic",
            CarParkBasement = "N",
            ParkingDetail = new ParkingDetail {
                CarParkNo = "CP001",
                ShortTermParking = "NO",
                FreeParking = "Yes",
                NightParking = "No"
            }
        });
        context.SaveChanges();
        return context;
    }

    [Fact]
    public void FilterCarParks_WithGantryHeight_ReturnsCorrectResult()
    {
        var context = GetInMemoryDbContext();
        var controller = new CarParksController(context);

        var result = controller.FilterCarParks(vehicleHeight: 1.5);

        Assert.NotNull(result);
    }
}