using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CustomerOrders.API.Converters
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private readonly string _format;
        private readonly CultureInfo _culture;

        public DateTimeJsonConverter(string format = "yyyy-MM-dd HH:mm:ss", string culture = "pt-BR")
        {
            _format = format;
            _culture = new CultureInfo(culture);
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }

            if (DateTime.TryParseExact(value, _format, _culture, DateTimeStyles.AssumeLocal, out var result))
            {
                return result;
            }

            return DateTime.Parse(value, _culture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            
            var local = value.Kind == DateTimeKind.Local ? value : value.ToLocalTime();
            writer.WriteStringValue(local.ToString(_format, _culture));
        }
    }
}


