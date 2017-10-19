using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using AutoFixture;
using TddEbook.TddToolkit.CommonTypes;
using TddEbook.TddToolkit.Subgenerators;
using StringGenerator = TddEbook.TddToolkit.Subgenerators.StringGenerator;

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
      var generator = new Fixture();
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