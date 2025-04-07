# WeatherApp

**WeatherApp** to konsolowa aplikacja napisana w C#, która pozwala na pobieranie danych pogodowych w czasie rzeczywistym z wykorzystaniem OpenWeatherMap API i przechowywanie ich lokalnie w bazie danych SQLite. W aplikacji zaimplementowano zarządzanie danymi, filtrowanie, sortowanie oraz podstawowe operacje CRUD.

---

## Spis treści

- [Funkcjonalności](#funkcjonalno%C5%9Bci)
- [Technologie](#technologie)
- [Instalacja i uruchomienie](#instalacja-i-uruchomienie)
- [Przykład użycia](#przyk%C5%82ad-u%C5%BCycia)
- [Struktura projektu](#struktura-projektu)
- [Model danych](#model-danych)
- [API i geolokalizacja](#api-i-geolokalizacja)
- [Uwaga dot. klucza API](#uwaga-dot-klucza-api)


---

## Funkcjonalności

-  Pobieranie aktualnej pogody dla podanego miasta
-  Geolokalizacja miasta do współrzędnych geograficznych (OpenWeatherMap Geocoding API)
-  Zapis danych pogodowych do bazy SQLite tylko wtedy, gdy nie istnieje już wpis dla miasta
-  Wyświetlanie wszystkich zapisanych rekordów
-  Sortowanie zapisanych miast wg temperatury (malejąco)
-  Filtrowanie rekordów wg kraju (kod kraju, np. "PL")
-  Usuwanie konkretnego miasta z bazy danych
-  Czyszczenie całej bazy danych

---

## Technologie

- C#
- .NET 7.0+
- Entity Framework Core
- SQLite
- OpenWeatherMap API (Weather + Geocoding)

---

## Instalacja i uruchomienie

1. **Sklonuj repozytorium:**

```bash
git clone https://github.com/twoj-login/weatherapp.git
cd weatherapp
```

2. **Przygotuj zależności:**

Upewnij się, że masz zainstalowane:
- .NET SDK
- EF Core Tools

Następnie zainstaluj pakiety NuGet:

```bash
dotnet restore
```

3. **Uruchom aplikację:**

```bash
dotnet run
```

4. **Wprowadź nazwę miasta**, a dane pogodowe zostaną automatycznie pobrane i zapisane w bazie `weather_data.db`.

---

## Przykład użycia

```text
Enter city name: Krakow
Fetching from API
 Id: 1, City: Krakow, Country: PL, Temp: 14.3°C, Desc: broken clouds, Wind: 2.5m/s
Sorted by temperature:
 Id: 1, City: Krakow, Country: PL, Temp: 14.3°C, Desc: broken clouds, Wind: 2.5m/s
By country:
Cities in PL:
 Id: 1, City: Krakow, Country: PL, Temp: 14.3°C, Desc: broken clouds, Wind: 2.5m/s
```

---

## Struktura projektu

```text
WeatherApp/
├── Program.cs               # Punkt startowy
├── ApiTest.cs               # Logika pobierania danych i operacje na bazie
├── WeatherData.cs           # Model danych pogodowych z API
├── GeoData.cs               # Dane geolokalizacyjne z API
├── WeatherRecord.cs         # Model encji bazy danych
├── WeatherDbContext.cs      # Kontekst Entity Framework
├── weather_data.db          # Plik bazy SQLite
├── WeatherApp.csproj        # Plik projektu
```

---

## Model danych

**Tabela `WeatherRecords` zawiera:**

| Pole         | Typ        | Opis                      |
|--------------|------------|---------------------------|
| `Id`         | int        | Klucz główny            |
| `City`       | string     | Nazwa miasta              |
| `Country`    | string     | Kod kraju (np. "PL")      |
| `Temperature`| float      | Temperatura w °C         |
| `Description`| string     | Opis pogody               |
| `WindSpeed`  | float      | Prędkość wiatru w m/s   |

---

## API i geolokalizacja

Aplikacja korzysta z dwóch punktów dostępowych OpenWeatherMap:

1. **Geocoding API** – zamienia nazwę miasta na współrzędne:
   `http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid=API_KEY`

2. **Weather API** – pobiera dane pogodowe:
   `http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=API_KEY&units=metric`

---

## Uwaga dot. klucza API

W pliku `ApiTest.cs` klucz API jest zapisany na stałe:

```csharp
private const string apiKey = "YOUR_API_KEY";
```





> Autor: [Aleksander Łyskawa]

