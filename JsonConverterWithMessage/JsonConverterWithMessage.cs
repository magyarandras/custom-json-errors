using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonConverterWithMessage;

public class JsonConverterWithErrorMessage<T> : JsonConverter<T>
{
    private string _message;

    public JsonConverterWithErrorMessage(string message)
    {
        _message = message;
    }

    public override T? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(ref reader, options);
        }
        catch (JsonException ex)
        {
            throw new JsonException(_message, ex);
        }

    }

    public override void Write(
        Utf8JsonWriter writer,
        T obj,
        JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, obj, options);
    }
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class JsonConverterWithMessageAttribute : JsonConverterAttribute
{
    private string _message;

    public JsonConverterWithMessageAttribute(string message)
    {
        this._message = message;
    }

    public override JsonConverter CreateConverter(Type typeToConvert)
    {
        var converterType = typeof(JsonConverterWithErrorMessage<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(converterType, _message)!;
    }
}