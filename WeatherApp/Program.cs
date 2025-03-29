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
        apiTest.ListAllWeatherRecords(); // This will display all the weather records
    }
}