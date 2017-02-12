using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using TddEbook.TddToolkit.Reflection;
using TddEbook.TypeReflection;
using TypeReflection.Interfaces;

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

      public static void NoStaticFields(Type type)
      {
        var staticFields = new List<IFieldWrapper>(SmartType.For(type).GetAllStaticFields());

        staticFields.Should()
                    .BeEmpty("SmartType " + type + " should not contain static fields, but: " + Environment.NewLine +
                             StringFrom(staticFields));
      }


      public static void NoStaticFields(Assembly assembly)
      {
        var staticFields = new List<IFieldWrapper>();
        foreach (var type in assembly.GetTypes())
        {
          staticFields.AddRange(SmartType.For(type).GetAllStaticFields());
        }

        staticFields.Should()
                    .BeEmpty("assembly " + assembly + " should not contain static fields, but: " + Environment.NewLine +
                             StringFrom(staticFields));
      }

      public static void NoReference(Assembly assembly1, Assembly assembly2)
      {
        assembly1.GetReferencedAssemblies().Should().Contain(assembly2.GetName(), "======" + assembly2.GetName().Name + " should not be referenced by " + 
          assembly1.GetName().Name  + " ======");
      }

      public static void NoReference(Assembly assembly1, Type type)
      {
        NoReference(assembly1, type.Assembly);
      }

      public static void NoNonPublicEvents(Assembly assembly)
      {
        var nonPublicEvents = new List<IEventWrapper>();
        
        foreach (var type in assembly.GetTypes())
        {
          nonPublicEvents.AddRange(SmartType.For(type).GetAllNonPublicEventsWithoutExplicitlyImplemented());
        }

        nonPublicEvents.Should()
                    .BeEmpty("assembly " + assembly + " should not contain non-public events, but: " + Environment.NewLine +
                             StringFrom(nonPublicEvents));
      }

      public static void SingleConstructor(Assembly assembly)
      {
        var constructorLimitsExceeded = new List<Tuple<Type, int>>();
        
        foreach (var type in assembly.GetTypes())
        {
          var constructorCount = SmartType.For(type).GetAllPublicConstructors().Count();
          if (constructorCount > 1)
          {
            constructorLimitsExceeded.Add(Tuple.Create(type, constructorCount)); 
          }
        }

        constructorLimitsExceeded.Any().Should()
                                 .BeFalse("assembly " + assembly +
                                          " should not contain types with more than one constructor, but: " +
                                          Environment.NewLine +
                                          StringFrom(constructorLimitsExceeded));
      }

      private static string StringFrom(IEnumerable<IFieldWrapper> staticFields)
      {
        var result = new HashSet<string>(staticFields.Select(f => f.GenerateExistenceMessage()));
        return String.Join(Environment.NewLine, result);
      }

      private static string StringFrom(IEnumerable<IEventWrapper> nonPublicEvents)
      {
        var result = new HashSet<string>(nonPublicEvents.Select(eventWrapper => eventWrapper.GenerateNonPublicExistenceMessage()));
        return String.Join(Environment.NewLine, result);
      }

      private static string StringFrom(IEnumerable<Tuple<Type, int>> constructorLimitsExceeded)
      {
        var limits = new HashSet<Tuple<Type, int>>(constructorLimitsExceeded);
        var result = limits.Select(l => l.Item1 + " contains " + l.Item2 + " constructors");
        return String.Join(Environment.NewLine, result);
      }

      public static void EnumHasUniqueValues<T>()
      {
        Enum.GetValues(typeof(T)).Should().OnlyHaveUniqueItems();
      }

      public static void HasUniqueConstants<T>()
      {
        string errors = "";
        var constants = SmartType.For(typeof(T)).GetAllConstants();
        foreach (var constant in constants)
        {
          foreach (var otherConstant in constants)
          {
            constant.AssertNotDuplicateOf(otherConstant);
          }
        }
      }
    }
}
