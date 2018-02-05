using System;
using System.Linq;
using Newtonsoft.Json;
using Sigfaz.Infra.ComponentModel.Extensions;

namespace Sigfaz.Infra.Mvc
{
    class EnumConverter : CustomBaseConverter
    {
        public EnumConverter(Type type) : base(type) { }

        public override bool CanConvert(Type type)
        {
            return NonNullable(type).IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            var str = reader.Value.ToString();
            objectType = NonNullable(objectType);
            var enums = Enum.GetValues(objectType).Cast<Enum>();
            var value = enums.FirstOrDefault(e => e.ToString().Equals(str) || e.ObterDescricao().Equals(str));
            if (value != null) return value;
            int i;
            if (!int.TryParse(str, out i)) return null;
            return Enum.ToObject(objectType, i);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteValue(value.ToString());
        }
    }
}