using HashCode.Genetic.Models;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HashCode.Genetic.Converters
{
    public class GeneticConfigurationJsonConverter : JsonConverter<GeneticConfiguration>
    {
        public override GeneticConfiguration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var configuration = reader.GetString().Split("_");

            return new GeneticConfiguration(Configurations.Selections.First(s => s.GetType().Name == configuration[0]),
                                            Configurations.Crossovers.First(c => c.GetType().Name == configuration[1]),
                                            Configurations.Mutations.First(m => m.GetType().Name == configuration[2]));
        }

        public override void Write(Utf8JsonWriter writer, GeneticConfiguration value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
