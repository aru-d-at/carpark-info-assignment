using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CarParkInfo.API.Models;

public class ParkingDetail
{
    [Key, ForeignKey("CarParkNo")]
    public string? CarParkNo { get; set; }
    public string ShortTermParking { get; set; } = string.Empty;
    public string FreeParking { get; set; } = string.Empty;
    public string NightParking { get; set; } = string.Empty;
}