using System;
using System.Linq;
using System.Reflection;

namespace Sigfaz.Infra.ComponentModel.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsClassOrSubclassOf<T>(this Type t)
        {
            return IsClassOrSubclassOf(t, typeof(T));
        }

        public static bool IsClassOrSubclassOf(this Type t, Type typeToCompare)
        {
            if (t == null) throw new ArgumentNullException("t");
            if (t == typeToCompare) return true;
            if (t.BaseType == null) return false;
            if (t.BaseType == typeToCompare) return true;
            if (t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeToCompare) return true;

            return IsClassOrSubclassOf(t.BaseType, typeToCompare);
        }

        public static bool ImplementsInterface<TInterface>(this Type t)
        {
            return ImplementsInterface(t, typeof(TInterface));
        }

        public static bool ImplementsInterface(this Type t, Type interfaceToCompare)
        {
            if (!interfaceToCompare.IsInterface)
                throw new ArgumentException("Tipo de comparação deve ser uma interface", "interfaceToCompare");

            return t.GetInterfaces().Contains(interfaceToCompare);
        }

        public static bool IsNullable(this Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static bool IsNullableEnum(this Type t)
        {
            return t.IsGenericType &&
                   t.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                   t.GetGenericArguments()[0].IsEnum;
        }

        public static Type GetEnumType(this Type t)
        {
            if (t.IsEnum)
                return t;
            if (t.IsNullableEnum())
                return t.GetGenericArguments()[0];
            throw new ArgumentException("Tipo deve ser uma enumeração!");
        }

        public static Type GetGenericArgument(this Type t, Type typeInterface)
        {
            if (!t.IsClassOrSubclassOf(typeInterface))
                throw new ArgumentException(String.Format("O tipo {0} não herda de {1}", t, typeInterface));

            while (!t.IsGenericType || t.GetGenericTypeDefinition() != typeInterface) 
            { 
                t = t.BaseType;

                if (t == typeof(object))
                    return null;
            }

            return t.GetGenericArguments()[0];
        }

        public static PropertyInfo GetPropertyFromPath(this Type type, string path)
        {
            PropertyInfo property = null;
            foreach (var propertyName in path.Split('.'))
            {
                property = type.GetProperty(propertyName);
                if (property == null) return null;
                type = property.PropertyType;
            }
            return property;
        }
    }
}
