using System.Text.Json.Serialization;
using System.Text.Json;

namespace TaxDeclaration
{
  public class PropertyConverter : JsonConverter<Property>
  {
    public override bool CanConvert(Type typeToConvert) =>
        typeof(Property).IsAssignableFrom(typeToConvert);

    public override Property Read(ref Utf8JsonReader reader,
        Type typeToConvert, JsonSerializerOptions options)
    {
      if (reader.TokenType != JsonTokenType.StartObject)
        throw new JsonException();
      reader.Read();
      if (reader.TokenType != JsonTokenType.PropertyName)
        throw new JsonException();
      string? propertyName = reader.GetString();
      if (propertyName != "PropertyType")
        throw new JsonException();
      reader.Read();
      if (reader.TokenType != JsonTokenType.String)
        throw new JsonException();
      var propertyType = reader.GetString();
      Property property;
      switch (propertyType)
      {
        case "Apartment":
          property = new Apartment();
          break;
        case "Store":
          property = new Store();
          break;
        case "Plot":
          property = new Plot();
          break;
        default:
          throw new JsonException();
      };

      while (reader.Read())
      {
        if (reader.TokenType == JsonTokenType.EndObject)
          return property;
        if (reader.TokenType == JsonTokenType.PropertyName)
        {
          propertyName = reader.GetString();
          reader.Read();
          switch (propertyName)
          {
            case "Id":
              property.Id = reader.GetInt32();
              break;
            case "Surface":
              property.Surface = reader.GetInt32();
              break;
            case "Floor":
              int floor = reader.GetInt32();
              if (property is Apartment)
                ((Apartment)property).Floor = floor;
              else
                throw new JsonException();
              break;
            case "Commerciality":
              int commerciality = reader.GetInt32();
              if (property is Store)
                ((Store)property).Commerciality = commerciality;
              else
                throw new JsonException();
              break;
            case "WithinCityLimits":
              bool withinCityLimits = reader.GetBoolean();
              if (property is Plot)
                ((Plot)property).WithinCityLimits = withinCityLimits;
              else
                throw new JsonException();
              break;
            case "Cultivated":
              bool cultivated = reader.GetBoolean();
              if (property is Plot)
                ((Plot)property).Cultivated = cultivated;
              else
                throw new JsonException();
              break;
            case "Address":
              if (reader.TokenType == JsonTokenType.StartObject)
              {
                Address address = new Address();
                while (reader.Read())
                {
                  if (reader.TokenType == JsonTokenType.EndObject)
                    break;
                  if (reader.TokenType == JsonTokenType.PropertyName)
                  {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                      case "Street":
                        address.Street = reader.GetString() ?? "";
                        break;
                      case "No":
                        address.No = reader.GetString() ?? "";
                        break;
                      case "Zip":
                        address.Zip = reader.GetString() ?? "";
                        break;
                      case "City":
                        address.City = reader.GetString() ?? "";
                        break;
                    }
                  }
                }
                property.Address = address;
              }
              break;
          }
        }
      }
      throw new JsonException();
    }
    public override void Write(Utf8JsonWriter writer,
        Property property, JsonSerializerOptions options)
    {
      writer.WriteStartObject();
      if (property is Apartment apartment)
      {
        writer.WriteString("PropertyType", "Apartment");
        writer.WriteNumber("Surface", apartment.Surface);
        writer.WriteNumber("Floor", apartment.Floor);
        writer.WriteStartObject("Address");
        writer.WriteString("Street", apartment.Address.Street);
        writer.WriteString("No", apartment.Address.No);
        writer.WriteString("Zip", apartment.Address.Zip);
        writer.WriteString("City", apartment.Address.City);
        writer.WriteEndObject();
      }
      else if (property is Store store)
      {
        writer.WriteString("PropertyType", "Store");
        writer.WriteNumber("Surface", store.Surface);
        writer.WriteNumber("Commerciality", store.Commerciality);
        writer.WriteStartObject("Address");
        writer.WriteString("Street", store.Address.Street);
        writer.WriteString("No", store.Address.No);
        writer.WriteString("Zip", store.Address.Zip);
        writer.WriteString("City", store.Address.City);
        writer.WriteEndObject();
      }
      else if (property is Plot plot)
      {
        writer.WriteString("PropertyType", "Plot");
        writer.WriteNumber("Surface", plot.Surface);
        writer.WriteBoolean("WithinCityLimits", plot.WithinCityLimits);
        writer.WriteBoolean("Cultivated", plot.Cultivated);
        writer.WriteStartObject("Address");
        writer.WriteString("Street", plot.Address.Street);
        writer.WriteString("No", plot.Address.No);
        writer.WriteString("Zip", plot.Address.Zip);
        writer.WriteString("City", plot.Address.City);
        writer.WriteEndObject();
      }
      writer.WriteEndObject();
    }
  }
}
