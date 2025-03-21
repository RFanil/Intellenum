using System.Collections.Generic;
using System.Linq;
using Intellenum.Generators.Snippets;
using Intellenum.MemberBuilding;
using Intellenum.StaticConstructorBuilding;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Intellenum.Generators;

public class ClassGenerator
{
    public static string BuildType(VoWorkItem item, TypeDeclarationSyntax tds, bool isNetFramework)
    {
        bool valueCanBeReadonly = item.MemberProperties.ValidMembers.All(m => m.WasExplicitlySetAValue && m.WasExplicitlySetAName);
        string @readonly = valueCanBeReadonly ? "readonly" : "";
        
        var typeName = tds.Identifier;

        List<string> interfaces = new(3)
        {
            $"global::System.IEquatable<{typeName}>"
        };
        
        if (item.IsUnderlyingIComparable)
        {
            interfaces.Add("global::System.IComparable");
        }
        
        if (item.IsUnderlyingIsIComparableOfT)
        {
            interfaces.Add($"global::System.IComparable<{typeName}>");
        }

        // Determine if the type is a class or struct
        bool isStruct = tds.Kind() == SyntaxKind.StructDeclaration;
        string typeKeyword = isStruct ? "struct" : "class";
        string itemUnderlyingType = item.UnderlyingTypeFullName;

        return
            $$"""

              using Intellenum;
              using System;
              {{Util.TryWriteNamespaceIfSpecified(item)}}

              {{Util.WriteStartNamespace(item.FullNamespace)}}
                  [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage] 
                  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{Util.GenerateYourAssemblyName()}}", "{{Util.GenerateYourAssemblyVersion()}}")]
                  {{Util.GenerateAnyConversionAttributes(tds, item)}}
                  {{Util.GenerateDebugAttributes(item, typeName, itemUnderlyingType)}}
                  {{Util.GenerateModifiersFor(tds)}} {{typeKeyword}} {{typeName}} : {{string.Join(",", interfaces)}} 
                  {
                      {{MemberGeneration.GenerateConstValuesIfPossible(item)}}
              
                      {{Util.GenerateLazyLookupsIfNeeded(item)}}

              #if DEBUG    
                      private readonly global::System.Diagnostics.StackTrace _stackTrace = null;
              #endif
                      private {{@readonly}} global::System.Boolean _isInitialized;
                      private {{@readonly}} {{itemUnderlyingType}} _value;
                      private {{@readonly}} global::System.String _name;
              
                      {{StaticInitializationBuilder.BuildIfNeeded(item)}}
              
                      /// <summary>
                      /// Gets the underlying <see cref="{{itemUnderlyingType}}" /> value if set, otherwise default
                      /// </summary>
                      public {{itemUnderlyingType}} Value => _value;
              
                      [global::System.Diagnostics.DebuggerStepThroughAttribute]
                      [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
                      public {{typeName}}()
                      {
              #if DEBUG
                          _stackTrace = new global::System.Diagnostics.StackTrace();
              #endif
                          _isInitialized = false;
                          _value = default;
                          _name = "[UNDEFINED]";
                      }
              
                      [global::System.Diagnostics.DebuggerStepThroughAttribute]
                      public {{typeName}}({{itemUnderlyingType}} value)
                      {
                          _value = value;
                          _name = "[INFERRED-TO-BE-REPLACED!]";
                          _isInitialized = true;
                      }        
              
                      [global::System.Diagnostics.DebuggerStepThroughAttribute]
                      private {{typeName}}(string enumName, {{itemUnderlyingType}} value)
                      {
                          _value = value;
                          _name = enumName;
                          _isInitialized = true;
                      }
              
                      public global::System.String Name => _name;
              
                      public void Deconstruct(out string Name, out {{itemUnderlyingType}} Value)
                      {
                          Name = this._name;
                          Value = this._value;
                      }
              
                      {{SnippetGenerationFactory.Generate(item, tds, SnippetType.FromValueRelateMethods, isNetFramework)}}
              
                      {{SnippetGenerationFactory.Generate(item, tds, SnippetType.FromNameRelatedMethods, isNetFramework)}}
              
                      {{EmitMemberMethods.Emit(item)}}
              
                      // only called internally when something has been deserialized into
                      // its primitive type.
                      private static {{typeName}} __Deserialize({{itemUnderlyingType}} value)
                      {
                          {{GenerateNullCheckIfNeeded(item)}}
              
                          {{Util.GenerateCallToValidateForDeserializing(item)}}
              
                          return FromValue(value);
                      }
              
                      [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                      public global::System.Boolean Equals({{typeName}} other)
                      {
                          if (ReferenceEquals(null, other))
                          {
                              return false;
                          }
              
                          // It's possible to create uninitialized members via converters such as EfCore (HasDefaultValue), which call Equals.
                          // We treat anything uninitialized as not equal to anything, even other uninitialized members of this type.
                          if(!_isInitialized || !other._isInitialized) return false;
              	    	
                          if (ReferenceEquals(this, other))
                          {
                              return true;
                          }
              
                          return GetType() == other.GetType() && global::System.Collections.Generic.EqualityComparer<{{itemUnderlyingType}}>.Default.Equals(Value, other.Value);
                      }
              
                      [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                      public override global::System.Boolean Equals(global::System.Object obj)
                      {
                          if (ReferenceEquals(null, obj))
                          {
                              return false;
                          }
              
                          if (ReferenceEquals(this, obj))
                          {
                              return true;
                          }
              
                          if (obj.GetType() != GetType())
                          {
                              return false;
                          }
              
                          return Equals(({{typeName}}) obj);
                      }
              
                      [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                      public static global::System.Boolean operator ==({{typeName}} left, {{typeName}} right) => Equals(left, right);
              
                      [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                      public static global::System.Boolean operator !=({{typeName}} left, {{typeName}} right) => !Equals(left, right);
              
                      [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                      public static global::System.Boolean operator ==({{typeName}} left, {{itemUnderlyingType}} right) => Equals(left.Value, right);
              
                      [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                      public static global::System.Boolean operator !=({{typeName}} left, {{itemUnderlyingType}} right) => !Equals(left.Value, right);
              
                      [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                      public static global::System.Boolean operator ==({{itemUnderlyingType}} left, {{typeName}} right) => Equals(left, right.Value);
              
                      [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                      public static global::System.Boolean operator !=({{itemUnderlyingType}} left, {{typeName}} right) => !Equals(left, right.Value);
                      
                      [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                      public static explicit operator {{typeName}}({{itemUnderlyingType}} value) => FromValue(value);
              
                      [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                      public static implicit operator {{itemUnderlyingType}}({{typeName}} value) => value.Value;
              
                      {{Util.GenerateIComparableImplementationIfNeeded(item, tds)}}
              
                      [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                      public override global::System.Int32 GetHashCode() => GetType().GetHashCode() ^ this._value.GetHashCode();
              
                      {{MemberGeneration.GenerateAnyMembers(tds, item)}}
                      
                      public static global::System.Collections.Generic.IEnumerable<{{typeName}}> List()
                      {
                          {{MemberGeneration.GenerateIEnumerableYields(item)}}
                      }        
              
                      {{Util.GenerateToString(item)}}
              
                      {{Util.GenerateAnyConversionBodies(tds, item)}}
                      
                      {{TryParseGeneration.GenerateTryParseIfNeeded(item)}}
              
                      {{Util.GenerateDebuggerProxyForClasses(tds, item)}}

                      internal static class ThrowHelper
                      {
                            internal static void ThrowCreatedWithNull() => throw new {{nameof(IntellenumCreationFailedException)}}("Cannot create an Intellenum member with a null.");
                            internal static void ThrowMatchFailed(string message) => throw new {{nameof(IntellenumMatchFailedException)}}(message);
                              
                      }
                  }
                  
              {{Util.WriteCloseNamespace(item.FullNamespace)}}
              """;
    }

    private static string GenerateNullCheckIfNeeded(VoWorkItem voWorkItem) =>
        voWorkItem.IsValueType
            ? string.Empty
            : $$"""
                            if (value is null)
                            {
                                ThrowHelper.ThrowCreatedWithNull();
                            }

                """;
}
