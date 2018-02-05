using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Sigfaz.Infra.ComponentModel.DataAnnotations;
using Sigfaz.Infra.ComponentModel.Extensions;

namespace Sigfaz.Infra.Mvc
{
    class DateConverter : CustomBaseConverter
    {
        public DateConverter(Type type) : base(type) { }

        public override bool CanConvert(Type type)
        {
            return NonNullable(type) == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value as string;
            
            if (String.IsNullOrEmpty(value)) 
                return null;

            var atributoDateTime = GetDateTimeAttribute(MemberInfoFromPath(reader.Path));

            return DateTime.ParseExact(value, DateTimeExtension.GetFormat(atributoDateTime), new DateTimeFormatInfo());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var atributoDateTime = GetDateTimeAttribute(MemberInfoFromPath(writer.Path));

            writer.WriteValue((value as DateTime?).ToFormattedString(atributoDateTime));
        }

        private static DateTimeAttribute GetDateTimeAttribute(MemberInfo propertyMetadata)
        {
            return propertyMetadata.GetCustomAttributes(true).OfType<DateTimeAttribute>().FirstOrDefault();
        }
    }
}