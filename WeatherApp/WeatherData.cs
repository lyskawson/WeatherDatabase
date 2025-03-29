namespace WeatherApp;

public class WeatherData
{
    public Main main { get; set; }  
    public Weather[] weather { get; set; } 
    public string? name { get; set; } 
    public Wind wind { get; set; }  
    
    public Sys sys { get; set; } 
    public int cod { get; set; } 

    public override string ToString()
    {
        return $"City: {name}, Country: {sys?.country}, Temperature: {main.temp}°C, " +
               $"Humidity: {main.humidity}%, Description: {weather[0].description}, " +
               $"Wind Speed: {wind.speed} m/s, Response Code: {cod}";
    }


    public class Main
    {
        public float temp { get; set; }
        public int humidity { get; set; }
        
    }
    
    public class Weather
    { 
        public string description { get; set; }
     
    }

    public class Wind
    { 
        public float speed { get; set; }  
       
    }

    public class Sys
    {
        public string? country { get; set; }
    }
}