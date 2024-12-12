namespace CarParkInfo.API.DTOs;

public class CarParkDto
{
    public string? CarParkNo { get; set; }
    public string? Address { get; set; }
    public double XCoord { get; set; }
    public double YCoord { get; set; }
    public string? CarParkType { get; set; }
    public string? TypeOfParkingSystem { get; set; }
    public int CarParkDecks { get; set; }
    public double GantryHeight { get; set; }
    public string? CarParkBasement { get; set; }
}

public class CarParkDetailedDto
{
    public string? CarParkNo { get; set; }
    public string? Address { get; set; }
    public double XCoord { get; set; }
    public double YCoord { get; set; }
    public string? CarParkType { get; set; }
    public string? TypeOfParkingSystem { get; set; }
    public int CarParkDecks { get; set; }
    public double GantryHeight { get; set; }
    public string? CarParkBasement { get; set; }
    public ParkingDetailDto? ParkingDetail { get; set; }
}

public class ParkingDetailDto
{
    public string? ShortTermParking { get; set; }
    public string? FreeParking { get; set; }
    public string? NightParking { get; set; }
}