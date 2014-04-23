namespace CodeBetter.Json.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal static class ReflectionHelper
    {
        private readonly static Type _includeBaseAttributeType = typeof (SerializeIncludingBaseAttribute);
        private static readonly Type _nonSerializableAttributeType = typeof(NonSerializedAttribute);

        public static List<FieldInfo> GetSerializableFields(Type type)
        {
            List<FieldInfo> fields = new List<FieldInfo>(10);
            fields.AddRange(type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)); //BindingFlags.NonPublic |
            RemoveNonSerializableFields(fields);
            if (type.BaseType != null && type.GetCustomAttributes(_includeBaseAttributeType, false).Length > 0)
            {
                fields.AddRange(GetSerializableFields(type.BaseType));
            }
            return fields;
        }

        public static List<PropertyInfo> GetSerializablePropertys(Type type)
        {
            List<PropertyInfo> propertys = new List<PropertyInfo>(10);
            propertys.AddRange(type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly));
            RemoveNonSerializablePropertys(propertys);
            if (type.BaseType != null && type.GetCustomAttributes(_includeBaseAttributeType, false).Length > 0)
            {
                propertys.AddRange(GetSerializablePropertys(type.BaseType));
            }
            return propertys;
        }

        private static void RemoveNonSerializableFields(List<FieldInfo> fields)
        {
            for(int i = 0; i < fields.Count; ++i)
            {
                if (!ShouldSerializeFieldAndPropertys(fields[i]))
                {
                    fields.RemoveAt(i--);
                }
            }
        }

        private static void RemoveNonSerializablePropertys(List<PropertyInfo> propertys)
        {
            for (int i = 0; i < propertys.Count; ++i)
            {
                if (!ShouldSerializeFieldAndPropertys(propertys[i]))
                {
                    propertys.RemoveAt(i--);
                }
            }
        }

        public static bool ShouldSerializeFieldAndPropertys(ICustomAttributeProvider field)
        {
            return field.GetCustomAttributes(_nonSerializableAttributeType, true).Length <= 0;
        }

        public static FieldInfo FindField(Type type, string name)
        {
            FieldInfo field = FindFieldThroughoutHierarchy(type, name);
            //if (field == null)
            //{
            //    throw new ArgumentException(type.FullName + " doesn't have a field named: " + name);
            //}
            return field;
        }

        public static PropertyInfo FindProperty(Type type, string name)
        {
            PropertyInfo property = type.GetProperty(name);
            //if (property == null)
            //{
            //    throw new ArgumentException(type.FullName + " doesn't have a property named: " + name)
            //}
            return property;
        }
        public static FieldInfo FindFieldThroughoutHierarchy(Type type, string name)
        {
            FieldInfo field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            if (field == null && type.GetCustomAttributes(_includeBaseAttributeType, false).Length > 0)
            {
                field = FindFieldThroughoutHierarchy(type.BaseType, name);
            }
            return field;
        }


        public static object GetValue(FieldInfo field, object @object)
        {
            object value = field.GetValue(@object);
            return (field.FieldType.IsEnum) ? (int) value : value;            
        }
        public static object GetValue(PropertyInfo property, object @object)
        {
            object value = property.GetValue(@object,null);
            return (property.PropertyType.IsEnum) ? (int)value : value;
        }
        public static ConstructorInfo GetDefaultConstructor(Type type)
        {
            ConstructorInfo constructor = type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
            if (constructor == null)
            {
                throw new JsonException(type.FullName + " must have a parameterless constructor");
            }
            return constructor;
        }
    }
}
