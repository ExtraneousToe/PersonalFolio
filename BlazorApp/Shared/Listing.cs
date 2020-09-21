using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorApp.Shared
{
    public enum ListingStatus
    {
        InDevelopment,
        DevelopmentCeased,
        DevelopmentPaused,
        Complete,
        Unknown,
        NumStatuses
    }

    public class ListingStatusConverter : JsonConverter<ListingStatus>
    {
        public override ListingStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.None:
                case JsonTokenType.StartObject:
                case JsonTokenType.EndObject:
                case JsonTokenType.StartArray:
                case JsonTokenType.EndArray:
                case JsonTokenType.PropertyName:
                case JsonTokenType.Comment:
                case JsonTokenType.True:
                case JsonTokenType.False:
                case JsonTokenType.Null:
                    return ListingStatus.Unknown;
                case JsonTokenType.String:
                    switch (reader.GetString())
                    {
                        case "InDevelopment":
                        case "In Development":
                            return ListingStatus.InDevelopment;
                        case "DevelopmentCeased":
                        case "Development Ceased":
                        case "Ceased":
                            return ListingStatus.DevelopmentCeased;
                        case "DevelopmentPaused":
                        case "Development Paused":
                        case "Paused":
                            return ListingStatus.DevelopmentPaused;
                        case "Complete":
                            return ListingStatus.Complete;
                        case "Unknown":
                            return ListingStatus.Unknown;
                    }
                    break;
                case JsonTokenType.Number:
                    return (ListingStatus)reader.GetInt32();
            }

            return ListingStatus.Unknown;
        }

        public override void Write(Utf8JsonWriter writer, ListingStatus value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue((int)value);
        }
    }

    public class Listing
    {
        public string Name { get; set; }
        public string[] Description { get; set; }
        public string RepositoryLink { get; set; }
        public string GHPagesLink { get; set; }

        [JsonConverter(typeof(ListingStatusConverter))]
        public ListingStatus Status { get; set; }

        public string StatusNotes { get; set; }

        [JsonIgnore]
        public string StatusAsString
        {
            get 
            {
                switch (Status)
                {
                    case ListingStatus.InDevelopment:
                        return "In Development";
                    case ListingStatus.DevelopmentCeased:
                        return "Development Ceased";
                    case ListingStatus.DevelopmentPaused:
                        return "Development Paused";
                    case ListingStatus.Complete:
                        return "Complete";
                    case ListingStatus.Unknown:
                    case ListingStatus.NumStatuses:
                    default:
                        return "Unknown";
                }
            }
        }

        [JsonIgnore]
        public string NameWithoutWhitespace => Name.Replace(" ", "");
    }
}
