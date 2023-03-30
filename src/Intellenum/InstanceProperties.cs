﻿namespace Intellenum;

public class InstanceProperties
{
    public InstanceProperties(InstanceSource source,
        string fieldName,
        string enumFriendlyName,
        string valueAsText,
        object value,
        string tripleSlashComments = "",
        bool explicitlyNamed = true)
    {
        Source = source;
        FieldName = fieldName;
        EnumEnumFriendlyName = enumFriendlyName;
        ValueAsText = valueAsText;
        Value = value;
        TripleSlashComments = tripleSlashComments;
        ExplicitlyNamed = explicitlyNamed;
    }

    public InstanceSource Source { get; }
    
    public string FieldName { get; }

    public string EnumEnumFriendlyName { get; }
    
    public string ValueAsText { get; }

    public object Value { get; }
    
    public string TripleSlashComments { get; }
    public bool ExplicitlyNamed { get; }
}
