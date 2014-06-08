using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentAssertions;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit
{
    public partial class XAssert
    {
      public static void AttributeExistsOnMethodOf<T>(
        Attribute attr, Expression<Action<T>> methodExpression)
      {
        var method = Method.Of(methodExpression);
        method.HasAttribute(attr.GetType(), attr).Should().BeTrue();
      }

      public static void AttributeExistsOnMethodOf<T, TAttr>(Expression<Action<T>> methodExpression)
      {
        var method = Method.Of(methodExpression);
        method.HasAttribute<TAttr>().Should().BeTrue();
      }

      public static void NoStaticFieldsIn(Type type)
      {
        var staticFields = new List<IFieldWrapper>(TypeWrapper.For(type).GetAllStaticFields());

        staticFields.Should()
                    .BeEmpty("Type " + type + " should not contain static fields, but: " + Environment.NewLine +
                             StringFrom(staticFields));
      }


      public static void NoStaticFieldsIn(Assembly assembly)
      {
        var staticFields = new List<IFieldWrapper>();
        foreach (var type in assembly.GetTypes())
        {
          staticFields.AddRange(TypeWrapper.For(type).GetAllStaticFields());
        }

        staticFields.Should()
                    .BeEmpty("assembly " + assembly + " should not contain static fields, but: " + Environment.NewLine +
                             StringFrom(staticFields));
      }

      private static string StringFrom(IEnumerable<IFieldWrapper> staticFields)
      {
        var result = "";
        foreach (var field in staticFields)
        {
          result += field.GenerateExistenceMessage()
            + Environment.NewLine;
        }
        return result;
      }


      public static void IsNotReferencedBy(Assembly assembly1, Assembly assembly2)
      {
        assembly1.GetReferencedAssemblies().Should().Contain(assembly2.GetName(), "======" + assembly2.GetName().Name + " should not be referenced by " + 
          assembly1.GetName().Name  + " ======");
      }

      public static void IsNotReferencedBy(Assembly assembly1, Type type)
      {
        IsNotReferencedBy(assembly1, type.Assembly);
      }
    }
}
