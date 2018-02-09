using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Sigfaz.Infra.Mvc
{
    abstract class CustomBaseConverter : JsonConverter
    {
        public override bool CanWrite { get { return true; } }

        public override bool CanRead { get { return true; } }

        private readonly Type objectType;

        protected CustomBaseConverter(Type type)
        {
            this.objectType = type;
        }

        public Type NonNullable(Type t)
        {
            var nullable = Nullable.GetUnderlyingType(t);
            if (nullable != null) return nullable;
            return t;
        }

        public Type TypeOfMember(MemberInfo m)
        {
            switch (m.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)m).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)m).PropertyType;
            }
            return null;
        }

        public MemberInfo MemberInfoFromPath(string path)
        {
            var props = path.Split('.');
            Type t = objectType;
            MemberInfo m = null;
            foreach (var prop in props)
            {
                var r = new Regex(@"([\w-[0-9]]\w*)?\[[0-9]+\]");
                if (r.IsMatch(prop))
                {
                    int start = 0;
                    while (r.IsMatch(prop, start))
                    {
                        var match = r.Match(prop, start);
                        var vetor = match.Groups[1].Captures.Count > 0 ? match.Groups[1].Captures[0].Value : null;
                        if (vetor != null)
                        {
                            m = t.GetMember(vetor).First();
                            t = TypeOfMember(m);
                        }
                        t = t.GetGenericArguments().First();
                        start = match.Groups[0].Index + match.Groups[0].Length;
                    }
                }
                else
                {
                    m = t.GetMember(prop).First();
                    t = TypeOfMember(m);
                }
            }
            return m;
        }
    }
}