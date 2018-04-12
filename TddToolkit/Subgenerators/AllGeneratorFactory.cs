using Castle.DynamicProxy;
using AutoFixture;
using AutoFixture.Kernel;
using TddEbook.TddToolkit.Generators;
using TddEbook.TddToolkit.TypeResolution;
using TddEbook.TddToolkit.TypeResolution.CustomCollections;
using TddEbook.TddToolkit.TypeResolution.FakeChainElements;
using StringGenerator = TddEbook.TddToolkit.Generators.StringGenerator;

namespace TddEbook.TddToolkit.Subgenerators
{
  internal static class AllGeneratorFactory
  {
    public static AllGenerator Create()
    {
      var emptyCollectionFixture = new Fixture
      {
        RepeatCount = 0
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
        emptyCollectionGenerator, //TODO make separate chain for this
        proxyGenerator, 
        new FakeChainFactory(
          new CachedReturnValueGeneration(new PerMethodCache<object>()), 
          GlobalNestingLimit.Of(5), 
          proxyGenerator, //TODO get rid of this dependency - its runtime-circular
          valueGenerator,
          collectionGenerator));
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
        new InvokableGenerator());
      fixtureConfiguration.ApplyTo(fixture, stringGenerator, numericGenerator);
      return allGenerator;
    }
  }
}