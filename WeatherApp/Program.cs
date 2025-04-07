namespace WeatherApp;
using System;
using System.Threading.Tasks;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Enter city name: ");
        string city = Console.ReadLine();

        ApiTest apiTest = new ApiTest();
        await apiTest.GetWeatherData(city);
        apiTest.ListAllWeatherRecords(); 
        
        Console.Write("Sorted by temperature: ");
        apiTest.GetSortedByTemperature();
        
        Console.Write("By country: ");
        apiTest.GetCitiesByCountry("PL");
    }
}