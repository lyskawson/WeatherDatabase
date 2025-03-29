namespace WeatherApp;

    using System;
    using System.Net.Http;
    using System.Text.Json;
    using System.Linq;
    using System.Threading.Tasks;

    public class ApiTest
    {
        private readonly HttpClient client = new HttpClient(); // Only assigned once
        private const string apiKey = "4fee8910aaf02db71352db420c76744d";

        public async Task GetWeatherData(string city)
        {
            try
            {
                // Check if data exists in the database
                using (var db = new WeatherDbContext())
                {
                    var existingRecord = db.WeatherRecords.FirstOrDefault(w => w.City == city); 
                    if (existingRecord != null)
                    {
                        Console.WriteLine("Data fetched from the database");
                        Console.WriteLine(existingRecord);
                        return;
                    }
                }

                Console.WriteLine("Fetching from API");

                (double lat, double lon) = await GetCoordinates(city);
                if (lat == 0 && lon == 0)
                {
                    Console.WriteLine(" Error: Could not find the city.");
                    return;
                }

                string weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";
                string response = await client.GetStringAsync(weatherUrl);
                WeatherData weatherData = JsonSerializer.Deserialize<WeatherData>(response);

                var newRecord = new WeatherRecord
                {
                    City = weatherData?.name,
                    Country = weatherData?.sys.country,
                    Temperature = weatherData.main.temp,
                    Description = weatherData.weather[0].description,
                    WindSpeed = weatherData.wind.speed,
                };

                // Save data to the database
                using (var db = new WeatherDbContext())
                {
                    db.WeatherRecords.Add(newRecord); // Updated to WeatherRecords
                    db.SaveChanges();
                }

                Console.WriteLine(newRecord);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task<(double, double)> GetCoordinates(string city)
        {
            try
            {
                string geoUrl = $"https://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={apiKey}";
                string response = await client.GetStringAsync(geoUrl);
                var locations = JsonSerializer.Deserialize<GeoData[]>(response);

                if (locations != null && locations.Length > 0)
                {
                    return (locations[0].lat, locations[0].lon);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Geocoding error: {ex.Message}");
            }

            return (0, 0); // Return invalid if error occurred
        }

        public void GetCitiesByCountry(string country)
        {
            using (var db = new WeatherDbContext())
            {
                var cities = db.WeatherRecords.Where(w => w.Country == country).ToList(); // Updated to WeatherRecords
                if (cities.Any())
                {
                    Console.WriteLine($"Cities in {country}:");
                    foreach (var city in cities)
                    {
                        Console.WriteLine(city);
                    }
                }
                else
                {
                    Console.WriteLine($"No cities in {country}.");
                }
            }
        }

        public void RemoveCity(string city)
        {
            using (var db = new WeatherDbContext())
            {
                var cityRecord = db.WeatherRecords.FirstOrDefault(w => w.City == city); // Updated to WeatherRecords
                if (cityRecord != null)
                {
                    db.WeatherRecords.Remove(cityRecord); // Updated to WeatherRecords
                    db.SaveChanges();
                    Console.WriteLine($"City {city} has been removed from the database");
                }
                else
                {
                    Console.WriteLine($"City not found");
                }
            }
        }

        // 🔹 Usuwanie wszystkich danych
        public void ClearDatabase()
        {
            using (var db = new WeatherDbContext())
            {
                db.WeatherRecords.RemoveRange(db.WeatherRecords); // Updated to WeatherRecords
                db.SaveChanges();
                Console.WriteLine("All database has been cleared");
            }
        }

        // 🔹 Sortowanie miast po temperaturze
        public void GetSortedByTemperature()
        {
            using (var db = new WeatherDbContext())
            {
                var sortedCities = db.WeatherRecords.OrderByDescending(w => w.Temperature).ToList(); // Updated to WeatherRecords
                if (sortedCities.Any())
                {
                    Console.WriteLine("Sorted by temperature");
                    foreach (var city in sortedCities)
                    {
                        Console.WriteLine(city);
                    }
                }
                else
                {
                    Console.WriteLine("No cities found.");
                }
            }
        }
        
        public void ListAllWeatherRecords()
        {
            using (var db = new WeatherDbContext())
            {
                var records = db.WeatherRecords.ToList();
                foreach (var record in records)
                {
                    Console.WriteLine($"City: {record.City}, Temperature: {record.Temperature}");
                }
            }
                
            
        }
    }
