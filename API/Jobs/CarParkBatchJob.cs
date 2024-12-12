using CarParkInfo.API.Data;


namespace CarParkInfo.API.Jobs
{
    public class CarParkBatchJob
    {
        private readonly CarParkContext _context;

        public CarParkBatchJob(CarParkContext context)
        {
            _context = context;
        }

        public void ProcessDailyDelta(string csvFilePath)
        {
            var lines = File.ReadAllLines(csvFilePath);
            var carParks = lines.Skip(1)
                .Select(line => DataSeeder.ParseCarPark(line))
                .ToList();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var carPark in carParks)
                    {
                        // Check for duplicates or conflicts
                        if (!_context.CarParks.Any(c => c.CarParkNo == carPark.CarParkNo))
                        {
                            _context.CarParks.Add(carPark);
                        }
                        else
                        {
                            throw new Exception($"Duplicate CarParkNo: {carPark.CarParkNo}");
                        }
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error processing batch job: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
