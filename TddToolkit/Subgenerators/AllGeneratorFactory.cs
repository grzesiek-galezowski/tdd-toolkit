using Castle.DynamicProxy;
using Ploeh.AutoFixture;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements;

namespace TddEbook.TddToolkit.Subgenerators
{
  internal static class AllGeneratorFactory
  {
    public static AllGenerator Create()
    {
      var emptyCollectionFixture = new Fixture
      {
        RepeatCount = 0,
      };
      var methodProxyCalls = new GenericMethodProxyCalls();
      var fixtureConfiguration = new AutoFixtureConfiguration();
      var fixture = fixtureConfiguration.CreateUnconfiguredInstance();
      var collectionGenerator = new CollectionGenerator(methodProxyCalls);
      var valueGenerator = new ValueGenerator(fixture);
      var charGenerator = new CharGenerator(valueGenerator);
      var specificTypeObjectGenerator = new SpecificTypeObjectGenerator(valueGenerator);
      var emptyCollectionGenerator = new EmptyCollectionGenerator(
        emptyCollectionFixture, 
        methodProxyCalls);
      var proxyGenerator = new ProxyGenerator();
      var proxyBasedGenerator = new ProxyBasedGenerator(
        emptyCollectionFixture, 
        methodProxyCalls, 
        emptyCollectionGenerator, 
        proxyGenerator, 
        new FakeChainFactory(
          new CachedReturnValueGeneration(new PerMethodCache<object>()), 
          GlobalNestingLimit.Of(5), 
          proxyGenerator,
          valueGenerator,
          methodProxyCalls));
      var stringGenerator = new StringGenerator(
        charGenerator, 
        valueGenerator, 
        specificTypeObjectGenerator);
      var numericGenerator = new NumericGenerator(
        valueGenerator);
      var allGenerator = new AllGenerator(valueGenerator, 
        charGenerator, 
        specificTypeObjectGenerator, 
        stringGenerator, 
        emptyCollectionGenerator, 
        numericGenerator, 
        proxyBasedGenerator, 
        collectionGenerator, 
        new InvokableGenerator(proxyBasedGenerator));
      fixtureConfiguration.ApplyTo(fixture, stringGenerator, numericGenerator);
      return allGenerator;
    }
  }
}