using CarParkInfo.API.Models;


namespace CarParkInfo.API.Data;

public static class DataSeeder
{
    public static void SeedDatabase(CarParkContext context, string csvFilePath)
    {
        var lines = File.ReadAllLines(csvFilePath);
        var carParks = lines.Skip(1) // Skip the header row
            .Select(line =>
            {

                // NOTE: Debugging
                // for (int i = 0; i < columns.Length; i++)
                // {
                //     Console.WriteLine($"  Column {i + 1}: {columns[i]}");
                // }

                return ParseCarPark(line);
            })
            .ToList();

        foreach (var carPark in carParks)
        {
            if (!context.CarParks.Any(c => c.CarParkNo == carPark.CarParkNo))
            {
                context.CarParks.Add(carPark);
            }
        }

        context.SaveChanges();
    }

    public static CarPark ParseCarPark(string line)
    {
        var columns = line.Split(',');
        return new CarPark
        {
            CarParkNo = ParseStr(columns[0]),
            Address = ParseStr(columns[1]),
            XCoord = ParseDouble(columns[2]),
            YCoord = ParseDouble(columns[3]),
            CarParkType = ParseStr(columns[4]),
            TypeOfParkingSystem = ParseStr(columns[5]),
            CarParkDecks = ParseInt(columns[9], 0), // Default to 0 if parsing fails
            GantryHeight = ParseDouble(columns[10], 0), // Default to 0.0 if parsing fails
            CarParkBasement = ParseStr(columns[11]) == "Y" ? "Y" : "N",
            ParkingDetail = new ParkingDetail
            {
                CarParkNo = ParseStr(columns[0]),
                ShortTermParking = ParseStr(columns[6]),
                FreeParking = ParseStr(columns[7]),
                NightParking = ParseStr(columns[8]),
            }
        };
    }

    private static string ParseStr(string value)
    {
        return value.Trim('"');
    }

    private static double ParseDouble(string value, double defaultValue = 0.0)
    {
        value = value.Trim('"');

        return double.TryParse(
            value,
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out var result
        ) ? result : defaultValue;
    }

    private static int ParseInt(string value, int defaultValue = 0)
    {
        value = value.Trim('"');

        return int.TryParse(value, out var result) ? result : defaultValue;
    }
}