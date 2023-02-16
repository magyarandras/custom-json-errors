# Custom JSON errors

A JsonConverter for System.Text.Json that enables setting custom serialization error messages.

Usage example:

```csharp
public class Event
{
    [JsonConverterWithMessage("Invalid event date!")]
    public DateTime EventDate { get; set; }

}
```

```csharp
string jsonString = @"{
	""EventDate"": ""Not a date"",
}";

try
{
    Event? evt = JsonSerializer.Deserialize<Event>(jsonString);

    if (evt != null)
    {
        Console.WriteLine(evt.EventDate);
    }
}
catch (JsonException ex)
{
    //Should be "Invalid event date!"
    Console.WriteLine(ex.Message);
}
```