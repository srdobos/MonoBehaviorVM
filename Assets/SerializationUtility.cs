using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class SerializationUtility
{
    private const BindingFlags SerializedBindingFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                                        BindingFlags.Instance;

    public static IEnumerable<FieldInfo> GetSerializedFieldsOfType<T>(System.Type componentType)
    {
        var typeofT = typeof(T);
        var applicableFields = componentType.GetFields(SerializedBindingFlags)
            .Where(f => typeofT.IsAssignableFrom(f.FieldType));
        var serializedFields = applicableFields.Where(f => IsPubliclySerializable(f) || IsPrivatelySerializable(f));
        return serializedFields;
    }

    private static bool IsPrivatelySerializable(FieldInfo f)
    {
        return f.IsPrivate && f.IsDefined(typeof(SerializeField), true);
    }

    private static bool IsPubliclySerializable(FieldInfo f)
    {
        return f.IsPublic && !f.IsDefined(typeof(NonSerializedAttribute), true);
    }
}