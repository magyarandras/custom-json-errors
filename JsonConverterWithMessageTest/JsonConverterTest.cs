using System.Text.Json;
using JsonConverterWithMessage;

namespace JsonConverterWithMessageTest;

public class Event
{
    [JsonConverterWithMessage("Invalid event date!")]
    public DateTime EventDate { get; set; }

}

[TestClass]
public class JsonConverterTest
{
    [TestMethod]
    public void TestConverterWithInvalidDate()
    {
        string jsonString = @"{
			""EventDate"": ""Not a date"",
		}";

        try
        {
            JsonSerializer.Deserialize<Event>(jsonString);
        }
        catch (JsonException ex)
        {
            Assert.AreEqual("Invalid event date!", ex.Message);
        }
        catch (Exception ex)
        {
            Assert.Fail(
                string.Format(
                    "Unexpected exception of type {0} caught: {1}",
                    ex.GetType(), ex.Message
                )
            );
        }
    }
}