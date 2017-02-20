using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Ploeh.AutoFixture;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace TddEbook.TddToolkit
{
  public class AutoFixtureFactory
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

    private readonly CircularList<MethodInfo> MethodList =
      CircularList.CreateStartingFromRandom(typeof(List<int>).GetMethods(BindingFlags.Public | BindingFlags.Instance));

    public Fixture CreateCustomAutoFixture()
    {
      var generator = new Fixture();
      generator.Register<Type>(() => _types.Next());
      generator.Register<MethodInfo>(() => MethodList.Next());
      generator.Register(() => new Exception(AllGenerator.CreateAllGenerator().String(), new Exception(AllGenerator.CreateAllGenerator().String())));
      generator.Register(
        () =>
          new IPAddress(new[]
            {AllGenerator.CreateAllGenerator().Octet(), AllGenerator.CreateAllGenerator().Octet(), AllGenerator.CreateAllGenerator().Octet(), AllGenerator.CreateAllGenerator().Octet()}));
      generator.Customize(new MultipleCustomization());
      return generator;
    }
  }
}