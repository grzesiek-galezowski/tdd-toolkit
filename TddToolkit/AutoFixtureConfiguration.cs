using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using AutoFixture;
using AutoFixture.Kernel;
using TddEbook.TddToolkit.CommonTypes;
using TddEbook.TddToolkit.Generators;
using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;
using StringGenerator = TddEbook.TddToolkit.Generators.StringGenerator;

namespace TddEbook.TddToolkit
{
  public class AutoFixtureConfiguration
  {
    private readonly CircularList<Type> _types = CircularList.CreateStartingFromRandom(
      typeof(Type1),
      typeof(Type2),
      typeof(Type3),
      typeof(Type4),
      typeof(Type5),
      typeof(Type6),
      typeof(Type7),
      typeof(Type8),
      typeof(Type9),
      typeof(Type10),
      typeof(Type11),
      typeof(Type12),
      typeof(Type13));

    private readonly CircularList<MethodInfo> _methodList =
      CircularList.CreateStartingFromRandom(typeof(List<int>).GetMethods(BindingFlags.Public | BindingFlags.Instance));

    public Fixture CreateUnconfiguredInstance()
    {
      var generator = new Fixture(new EngineWithReplacedQuery());
      return generator;
    }

    public void ApplyTo(Fixture generator, StringGenerator stringGenerator, NumericGenerator numericGenerator)
    {
      generator.Register(() => _types.Next());
      generator.Register(() => _methodList.Next());
      generator.Register(() => new Exception(stringGenerator.String(), new Exception(stringGenerator.String())));
      generator.Register(
        () =>
          new IPAddress(new[]
            {numericGenerator.Octet(), numericGenerator.Octet(), numericGenerator.Octet(), numericGenerator.Octet()}));
      generator.Customize(new MultipleCustomization());
    }
  }

  public class Type1 { }
  public class Type2 { }
  public class Type3 { }
  public class Type4 { }
  public class Type5 { }
  public class Type6 { }
  public class Type7 { }
  public class Type8 { }
  public class Type9 { }
  public class Type10 { }
  public class Type11 { }
  public class Type12 { }
  public class Type13 { }
}

public class EngineWithReplacedQuery : DefaultEngineParts
{
  public override IEnumerator<ISpecimenBuilder> GetEnumerator()
  {
    using (var enumerator = base.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        var value = enumerator.Current;

        // Replace target method query
        if (value is MethodInvoker mi &&
            mi.Query is CompositeMethodQuery cmq &&
            cmq.Queries.Skip(1).FirstOrDefault() is FactoryMethodQuery)
        {
          yield return new MethodInvoker(
            new CompositeMethodQuery(
              new ModestConstructorQuery(),
              new PatchedFactoryMethodQuery()
            )
          );
        }
        else
        {
          yield return value;
        }
      }
    }
  }
}

public class PatchedFactoryMethodQuery : IMethodQuery
{
  public IEnumerable<IMethod> SelectMethods(Type type)
  {
    var factoryMethods = SmartType.For(type).TryToObtainPublicStaticFactoryMethodWithoutRecursion()
      .Where(m => m.HasNonPointerArgumentsOnly())
      .Where(m => !m.IsParameterless())
      .OrderBy(m => m.GetParametersCount());
    return factoryMethods;
  }

  private static bool IsNotExplicitCast(MethodInfo mi)
  {
    return !string.Equals(mi.Name, "op_Explicit", StringComparison.Ordinal);
  }

  private static bool isNotImplicitCast(MethodInfo mi)
  {
    return !string.Equals(mi.Name, "op_Implicit", StringComparison.Ordinal);
  }

  private static bool IsFactoryMethod(Type type, MethodInfo mi)
  {
    return mi.ReturnType == type;
  }
}