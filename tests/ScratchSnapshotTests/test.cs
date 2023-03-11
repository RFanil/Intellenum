// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a source generator named Intellenum (https://github.com/SteveDunn/Intellenum)
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

// Suppress warnings about [Obsolete] member usage in generated code.
#pragma warning disable CS0618

// Suppress warnings for 'Override methods on comparable types'.
#pragma warning disable CA1036

// Suppress Error MA0097 : A class that implements IComparable<T> or IComparable should override comparison operators
#pragma warning disable MA0097

// Suppress warning for 'The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. Auto-generated code requires an explicit '#nullable' directive in source.'
// The generator copies signatures from the BCL, e.g. for `TryParse`, and some of those have nullable annotations.
#pragma warning disable CS8669

// Suppress warnings about CS1591: Missing XML comment for publicly visible type or member 'Type_or_Member'
#pragma warning disable CS1591

using Intellenum;

namespace Whatever
{

    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage] 
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Intellenum", "0.0.0.0")]
    [global::System.Text.Json.Serialization.JsonConverter(typeof(CustomerTypeSystemTextJsonConverter))]
[global::System.ComponentModel.TypeConverter(typeof(CustomerTypeTypeConverter))]

    [global::System.Diagnostics.DebuggerTypeProxyAttribute(typeof(CustomerTypeDebugView))]
    [global::System.Diagnostics.DebuggerDisplayAttribute("Underlying type: System.Int32, Value = { _value }")]
    public partial struct CustomerType : global::System.IEquatable<CustomerType>, global::System.IEquatable<System.Int32> ,  global::System.IComparable<CustomerType>, global::System.IComparable
    {
#if DEBUG    
        private readonly global::System.Diagnostics.StackTrace _stackTrace = null;
#endif

        private readonly global::System.Boolean _isInitialized;
        
        private readonly System.Int32 _value;

        /// <summary>
        /// Gets the underlying <see cref="System.Int32" /> value if set, otherwise a <see cref="IntellenumValidationException" /> is thrown.
        /// </summary>
        public readonly System.Int32 Value
        {
            [global::System.Diagnostics.DebuggerStepThroughAttribute]
            get
            {
                EnsureInitialized();
                return _value;
            }
        }

        [global::System.Diagnostics.DebuggerStepThroughAttribute]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public CustomerType()
        {
#if DEBUG
            _stackTrace = new global::System.Diagnostics.StackTrace();
#endif

            _isInitialized = false;
            _value = default;
        }

        [global::System.Diagnostics.DebuggerStepThroughAttribute]
        private CustomerType(System.Int32 value) 
        {
            _value = value;
            _isInitialized = true;
        }

        
        /// <summary>
        /// Builds an instance from a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>An instance of this type.</returns>
        public static CustomerType FromValue(System.Int32 value)
        {
            return  From(value);
        }

        /// <summary>
        /// Builds an instance from the provided underlying type.
        /// </summary>
        /// <param name="value">The underlying type.</param>
        /// <returns>An instance of this type.</returns>
        private static CustomerType From(System.Int32 value)
        {
            

            CustomerType instance = new CustomerType(value);

            

            return instance;
        }

        public static explicit operator CustomerType(System.Int32 value) => From(value);
        public static explicit operator System.Int32(CustomerType value) => value.Value;

        // only called internally when something has been deserialized into
        // its primitive type.
        private static CustomerType Deserialize(System.Int32 value)
        {
            

                    if(value == Normal.Value) return Normal;
        if(value == Gold.Value) return Gold;
        if(value == Diamond.Value) return Diamond;


            return new CustomerType(value);
        }

        public readonly global::System.Boolean Equals(CustomerType other)
        {
            // It's possible to create uninitialized instances via converters such as EfCore (HasDefaultValue), which call Equals.
            // We treat anything uninitialized as not equal to anything, even other uninitialized instances of this type.
            if(!_isInitialized || !other._isInitialized) return false;

            return global::System.Collections.Generic.EqualityComparer<System.Int32>.Default.Equals(Value, other.Value);
        }

        public readonly global::System.Boolean Equals(System.Int32 primitive) => Value.Equals(primitive);

        public readonly override global::System.Boolean Equals(global::System.Object obj)
        {
            return obj is CustomerType && Equals((CustomerType) obj);
        }

        public static global::System.Boolean operator ==(CustomerType left, CustomerType right) => Equals(left, right);
        public static global::System.Boolean operator !=(CustomerType left, CustomerType right) => !(left == right);

        public static global::System.Boolean operator ==(CustomerType left, System.Int32 right) => Equals(left.Value, right);
        public static global::System.Boolean operator !=(CustomerType left, System.Int32 right) => !Equals(left.Value, right);

        public static global::System.Boolean operator ==(System.Int32 left, CustomerType right) => Equals(left, right.Value);
        public static global::System.Boolean operator !=(System.Int32 left, CustomerType right) => !Equals(left, right.Value);

        public int CompareTo(CustomerType other) => Value.CompareTo(other.Value);
        public int CompareTo(object other) {
            if(other == null) return 1;
            if(other is CustomerType x) return CompareTo(x);
            throw new global::System.ArgumentException("Cannot compare to object as it is not of type CustomerType", nameof(other));
        }

        
    /// <inheritdoc cref="int.TryParse(System.ReadOnlySpan{char}, System.Globalization.NumberStyles, System.IFormatProvider?, out int)"/>
    /// <summary>
    /// </summary>
    /// <returns>
    /// The value created via the <see cref="From"/> method.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">Thrown when the value can be parsed, but is not valid.</exception>
    public static global::System.Boolean TryParse(global::System.ReadOnlySpan<char> s, global::System.Globalization.NumberStyles style, global::System.IFormatProvider provider, 
#if NETCOREAPP3_0_OR_GREATER
[global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)]
#endif
 out CustomerType result) {
        if(System.Int32.TryParse(s, style, provider, out var r)) {
            result = From(r);
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc cref="int.TryParse(System.ReadOnlySpan{char}, System.IFormatProvider?, out int)"/>
    /// <summary>
    /// </summary>
    /// <returns>
    /// The value created via the <see cref="From"/> method.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">Thrown when the value can be parsed, but is not valid.</exception>
    public static global::System.Boolean TryParse(global::System.ReadOnlySpan<char> s, global::System.IFormatProvider provider, 
#if NETCOREAPP3_0_OR_GREATER
[global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)]
#endif
 out CustomerType result) {
        if(System.Int32.TryParse(s, provider, out var r)) {
            result = From(r);
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc cref="int.TryParse(System.ReadOnlySpan{char}, out int)"/>
    /// <summary>
    /// </summary>
    /// <returns>
    /// The value created via the <see cref="From"/> method.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">Thrown when the value can be parsed, but is not valid.</exception>
    public static global::System.Boolean TryParse(global::System.ReadOnlySpan<char> s, 
#if NETCOREAPP3_0_OR_GREATER
[global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)]
#endif
 out CustomerType result) {
        if(System.Int32.TryParse(s, out var r)) {
            result = From(r);
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc cref="int.TryParse(string?, System.Globalization.NumberStyles, System.IFormatProvider?, out int)"/>
    /// <summary>
    /// </summary>
    /// <returns>
    /// The value created via the <see cref="From"/> method.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">Thrown when the value can be parsed, but is not valid.</exception>
    public static global::System.Boolean TryParse(string s, global::System.Globalization.NumberStyles style, global::System.IFormatProvider provider, 
#if NETCOREAPP3_0_OR_GREATER
[global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)]
#endif
 out CustomerType result) {
        if(System.Int32.TryParse(s, style, provider, out var r)) {
            result = From(r);
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc cref="int.TryParse(string?, System.IFormatProvider?, out int)"/>
    /// <summary>
    /// </summary>
    /// <returns>
    /// The value created via the <see cref="From"/> method.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">Thrown when the value can be parsed, but is not valid.</exception>
    public static global::System.Boolean TryParse(string s, global::System.IFormatProvider provider, 
#if NETCOREAPP3_0_OR_GREATER
[global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)]
#endif
 out CustomerType result) {
        if(System.Int32.TryParse(s, provider, out var r)) {
            result = From(r);
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc cref="int.TryParse(string?, out int)"/>
    /// <summary>
    /// </summary>
    /// <returns>
    /// The value created via the <see cref="From"/> method.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">Thrown when the value can be parsed, but is not valid.</exception>
    public static global::System.Boolean TryParse(string s, 
#if NETCOREAPP3_0_OR_GREATER
[global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)]
#endif
 out CustomerType result) {
        if(System.Int32.TryParse(s, out var r)) {
            result = From(r);
            return true;
        }

        result = default;
        return false;
    }


        public readonly override global::System.Int32 GetHashCode() => global::System.Collections.Generic.EqualityComparer<System.Int32>.Default.GetHashCode(_value);

        /// <summary>Returns the string representation of the underlying type</summary>
    /// <inheritdoc cref="System.Int32.ToString()" />
    public readonly override global::System.String ToString() => Value.ToString();

        private readonly void EnsureInitialized()
        {
            if (!_isInitialized)
            {
#if DEBUG
                global::System.String message = "Use of uninitialized Value Object at: " + _stackTrace ?? "";
#else
                global::System.String message = "Use of uninitialized Value Object.";
#endif

                throw new global::Intellenum.IntellenumValidationException(message);
            }
        }

        
// instance...

public static readonly CustomerType Normal = new CustomerType(0);

// instance...

public static readonly CustomerType Gold = new CustomerType(1);

// instance...

public static readonly CustomerType Diamond = new CustomerType(2);

 
        
        class CustomerTypeSystemTextJsonConverter : global::System.Text.Json.Serialization.JsonConverter<CustomerType>
        {
            public override CustomerType Read(ref global::System.Text.Json.Utf8JsonReader reader, global::System.Type typeToConvert, global::System.Text.Json.JsonSerializerOptions options)
            {
                return CustomerType.Deserialize(reader.GetInt32());
            }

            public override void Write(System.Text.Json.Utf8JsonWriter writer, CustomerType value, global::System.Text.Json.JsonSerializerOptions options)
            {
                writer.WriteNumberValue(value.Value);
            }
        }


        class CustomerTypeTypeConverter : global::System.ComponentModel.TypeConverter
        {
            public override global::System.Boolean CanConvertFrom(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Type sourceType)
            {
                return sourceType == typeof(global::System.Int32) || sourceType == typeof(global::System.String) || base.CanConvertFrom(context, sourceType);
            }

            public override global::System.Object ConvertFrom(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Globalization.CultureInfo culture, global::System.Object value)
            {
                return value switch
                {
                    global::System.Int32 intValue => CustomerType.Deserialize(intValue),
                    global::System.String stringValue when !global::System.String.IsNullOrEmpty(stringValue) && global::System.Int32.TryParse(stringValue, out var result) => CustomerType.Deserialize(result),
                    _ => base.ConvertFrom(context, culture, value),
                };
            }

            public override bool CanConvertTo(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Type sourceType)
            {
                return sourceType == typeof(global::System.Int32) || sourceType == typeof(global::System.String) || base.CanConvertTo(context, sourceType);
            }

            public override object ConvertTo(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Globalization.CultureInfo culture, global::System.Object value, global::System.Type destinationType)
            {
                if (value is CustomerType idValue)
                {
                    if (destinationType == typeof(global::System.Int32))
                    {
                        return idValue.Value;
                    }

                    if (destinationType == typeof(global::System.String))
                    {
                        return idValue.Value.ToString();
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }





        internal sealed class CustomerTypeDebugView
        {
            private readonly CustomerType _t;

            CustomerTypeDebugView(CustomerType t)
            {
                _t = t;
            }

            public global::System.Boolean IsInitialized => _t._isInitialized;
            public global::System.String UnderlyingType => "System.Int32";
            public global::System.String Value => _t._isInitialized ? _t._value.ToString() : "[not initialized]" ;

            #if DEBUG
            public global::System.String CreatedWith => _t._stackTrace?.ToString() ?? "the From method";
            #endif

            public global::System.String Conversions => @"Default";
                }

}
}