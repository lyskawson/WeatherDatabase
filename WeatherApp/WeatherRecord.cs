namespace WeatherApp;
using System.ComponentModel.DataAnnotations;

internal class WeatherRecord
{ 
    public int Id { get; set; } 
    public string? City { get; set; }
    public string? Country { get; set; }
    public float Temperature { get; set; }
    public string Description { get; set; }
    public float WindSpeed { get; set; }

    public override string ToString()
    {
        return $" Id: {Id}, City: {City}, Country: {Country}, Temp: {Temperature}°C, Desc: {Description}, Wind: {WindSpeed}m/s";
    }
}