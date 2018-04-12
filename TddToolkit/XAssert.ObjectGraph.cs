using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Albedo;
using FluentAssertions;
using KellermanSoftware.CompareNetObjects;
using TddEbook.TddToolkit.CommonTypes;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.Reflection;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit
{
  public partial class XAssert
  {
    public static void Alike<T>(T expected, T actual)
    {
      var comparison = ObjectGraph.Comparison();
      var result = comparison.Compare(expected, actual);
      result.ExceededDifferences.Should().BeFalse(result.DifferencesString);
    }

    public static void NotAlike<T>(T expected, T actual)
    {
      var comparison = ObjectGraph.Comparison();
      var result = comparison.Compare(expected, actual);
      result.ExceededDifferences.Should().BeTrue(result.DifferencesString);
    }

    public static void Alike<T>(T expected, T actual, params Expression<Func<T, object>>[] skippedPropertiesOrFields)
    {
      var result = Compare(expected, actual, skippedPropertiesOrFields);
      result.ExceededDifferences.Should().BeFalse(result.DifferencesString);
    }

    public static void NotAlike<T>(T expected, T actual, params Expression<Func<T, object>>[] skippedPropertiesOrFields)
    {
      var result = Compare(expected, actual, skippedPropertiesOrFields);
      result.ExceededDifferences.Should().BeTrue(result.DifferencesString);
    }

    public static void Alike<T>(T expected, T actual, params string[] skippedPropertiesOrFields)
    {
      var result = Compare(expected, actual, skippedPropertiesOrFields);
      result.ExceededDifferences.Should().BeFalse(result.DifferencesString);
    }
    public static void NotAlike<T>(T expected, T actual, params string[] skippedPropertiesOrFields)
    {
      var result = Compare(expected, actual, skippedPropertiesOrFields);
      result.ExceededDifferences.Should().BeTrue(result.DifferencesString);
    }

    public static void Contains(Object o, Type t)
    {
      var propertiesAndFields = o.GetType().GetPropertiesAndFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.NonPublic);
      new SearchVisitor(o, t);
    }

    private static ComparisonResult Compare<T>(T expected, T actual, string[] skippedPropertiesOrFields)
    {
      var comparison = ObjectGraph.Comparison();
      foreach (var skippedMember in skippedPropertiesOrFields)
      {
        comparison.Config.MembersToIgnore.Add(skippedMember);
      }
      var result = comparison.Compare(expected, actual);
      return result;
    }

    private static ComparisonResult Compare<T>(T expected, T actual, IEnumerable<Expression<Func<T, object>>> skippedPropertiesOrFields)
    {
      var comparison = ObjectGraph.Comparison();
      foreach (var skippedPropertyOrField in skippedPropertiesOrFields)
      {
        var property = Property.FromUnaryExpression(skippedPropertyOrField);
        var field = Field.FromUnaryExpression(skippedPropertyOrField);
        if (property.HasValue)
        {
          Ignore(property, comparison);
        }
        else if (field.HasValue)
        {
          Ignore(field, comparison);
        }
      }
      var result = comparison.Compare(expected, actual);
      return result;
    }

    private static void Ignore(Maybe<Field> field, ICompareLogic comparison)
    {
      comparison.Config.MembersToIgnore.Add(field.Value.Name);
    }

    private static void Ignore(Maybe<Property> property, ICompareLogic comparison)
    {
      comparison.Config.MembersToIgnore.Add(property.Value.Name);
      comparison.Config.MembersToIgnore.Add($"<{property.Value.Name}>k__BackingField");
    }


  }

  public class SearchVisitor : ReflectionVisitor<Boolean>
  {
    private readonly object target;
    private readonly Type _searchedType;
    private bool value = false;

    public SearchVisitor(object target, Type searchedType)
    {
      this.target = target;
      _searchedType = searchedType;
    }

    public override bool Value
    {
      get { return this.value; }
    }

    public override IReflectionVisitor<bool> Visit(
      FieldInfoElement fieldInfoElement)
    {
      if (_searchedType == fieldInfoElement.FieldInfo.FieldType)
      {
        value = true;
      }
      return this;
    }

    public override IReflectionVisitor<bool> Visit(
      PropertyInfoElement propertyInfoElement)
    {
      if (_searchedType == propertyInfoElement.PropertyInfo.PropertyType)
      {
        value = true;
      }
      return this;
    }
  }
}
