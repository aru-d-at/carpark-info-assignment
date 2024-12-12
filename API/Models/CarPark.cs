using System.ComponentModel.DataAnnotations;


namespace CarParkInfo.API.Models;

public class CarPark
{
    [Key]
    public string? CarParkNo { get; set; }
    public string? Address { get; set; }
    public double XCoord { get; set; }
    public double YCoord { get; set; }
    public string? CarParkType { get; set; }
    public string? TypeOfParkingSystem { get; set; }
    public int CarParkDecks { get; set; }
    public double GantryHeight { get; set; }
    public string? CarParkBasement { get; set; }
    public ParkingDetail ParkingDetail { get; set; } = null!;
}