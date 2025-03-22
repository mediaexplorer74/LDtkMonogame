namespace LDtk.Parsers;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Xna.Framework;

class ColorConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string str = reader.GetString()!;

        if (str == null)
        {
            return default;
        }

        if (str.StartsWith('#'))
        {
            byte r = Convert.ToByte(str.Substring(1, 2), 16);
            byte g = Convert.ToByte(str.Substring(3, 2), 16);
            byte b = Convert.ToByte(str.Substring(5, 2), 16);
            return new(r, g, b, (byte)255);
        }

        throw new LDtkException(str);
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        string str = "#" + value.R.ToString("X2") + value.G.ToString("X2") + value.B.ToString("X2");
        writer.WriteStringValue(str);
    }
}
