using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Ploeh.AutoFixture;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;
using TddEbook.TddToolkit.Subgenerators;

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

    public Fixture CreateCustomAutoFixture(AllGenerator allGenerator)
    {
      var generator = new Fixture();
      generator.Register(() => _types.Next());
      generator.Register(() => MethodList.Next());
      generator.Register(() => new Exception(allGenerator.String(), new Exception(allGenerator.String())));
      generator.Register(
        () =>
          new IPAddress(new[]
            {allGenerator.Octet(), allGenerator.Octet(), allGenerator.Octet(), allGenerator.Octet()}));
      generator.Customize(new MultipleCustomization());
      return generator;
    }
  }
}