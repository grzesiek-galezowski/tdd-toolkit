using System;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using TddEbook.TddToolkit.NSubstitute;

namespace TddEbook.TddToolkitSpecification.NSubstituteSpecifications
{
  public class XReceivedSpecification
  {
    public class OnlySpecification
    {
      private IFoo _foo;
      private IBar _bar;

      [SetUp]
      public void SetUp()
      {
        _foo = Substitute.For<IFoo>();
        _bar = Substitute.For<IBar>();
      }

      [Test]
      public void Pass_when_checking_a_single_call()
      {
        _foo.Start();
        XReceived.Only(() => _foo.Start());
      }

      [Test]
      public void Fail_when_checking_a_single_call_that_was_not_made()
      {
        _foo.Start();

        Assert.Throws<CallSequenceNotFoundException>(() =>
          XReceived.Only(() => _foo.Finish())
          );
      }

      [Test]
      public void Pass_when_calls_match_exactly_even_in_different_order()
      {
        _bar.End();
        _foo.Start(2);
        _foo.Finish();
        _bar.Begin();

        XReceived.Only(() =>
        {
          _foo.Start(2);
          _bar.Begin();
          _foo.Finish();
          _bar.End();
        });
      }

      [Test]
      public void Fail_when_call_arg_does_not_match()
      {
        _foo.Start(1);
        _bar.Begin();
        _foo.Finish();
        _bar.End();

        Assert.Throws<CallSequenceNotFoundException>(() =>
            XReceived.Only(() =>
            {
              _foo.Start(2);
              _bar.Begin();
              _foo.Finish();
              _bar.End();
            })
            );
      }

      [Test]
      public void Use_arg_matcher()
      {
        _foo.Start(1);
        _bar.Begin();
        _foo.Finish();
        _bar.End();

        XReceived.Only(() =>
        {
          _foo.Start(Arg.Is<int>(x => x < 10));
          _bar.Begin();
          _foo.Finish();
          _bar.End();

        });
      }

      [Test]
      public void Fail_when_one_of_the_calls_was_not_received()
      {
        _foo.Start();
        _foo.Finish();

        Assert.Throws<CallSequenceNotFoundException>(() =>
            XReceived.Only(() =>
            {
              _foo.Start();
              _foo.FunkyStuff("hi");
              _foo.Finish();
            })
        );
      }

      [Test]
      public void Fail_when_additional_related_calls_at_start()
      {
        _foo.Start();
        _foo.Start();
        _bar.Begin();
        _foo.Finish();
        _bar.End();

        Assert.Throws<CallSequenceNotFoundException>(() =>
            XReceived.Only(() =>
            {
              _foo.Start();
              _bar.Begin();
              _foo.Finish();
              _bar.End();
            })
        );
      }

      [Test]
      public void Fail_when_additional_related_calls_at_end()
      {
        _foo.Start();
        _bar.Begin();
        _foo.Finish();
        _bar.End();
        _foo.Start();

        Assert.Throws<CallSequenceNotFoundException>(() =>
            XReceived.Only(() =>
            {
              _foo.Start();
              _bar.Begin();
              _foo.Finish();
              _bar.End();
            })
        );
      }

      [Test]
      public void Ignore_calls_from_unrelated_substitutes()
      {
        _bar.Begin();
        _foo.FunkyStuff("get funky!");
        _bar.End();

        XReceived.Only(() =>
        {
          _bar.Begin();
          _bar.End();
        });
      }


      [Test]
      public void Check_auto_subbed_props()
      {
        _foo.Start();
        _bar.Baz.Flurgle = "hi";
        _foo.Finish();


        XReceived.Only(() =>
        {
          _foo.Start();
          _bar.Baz.Flurgle = "hi";
          _foo.Finish();
        });
      }

      [Test]
      public void Fail_when_auto_subbed_prop_call_not_received()
      {
        _foo.Start();
        _bar.Baz.Flurgle = "hi";
        _foo.Finish();


        Assert.Throws<CallSequenceNotFoundException>(() =>
            XReceived.Only(() =>
            {
              _foo.Start();
              _bar.Baz.Flurgle = "howdy";
              _foo.Finish();
            })
         );
      }

      [Test]
      public void Verifying_calls_should_ignore_property_getter_calls()
      {
        var baz = _bar.Baz;
        baz.Wurgle();
        baz.Slurgle();

        XReceived.Only(() =>
        {
          // This call spec should be regarded as matching the
          // calling code above. So needs to ignore the get 
          // request to _bar.Baz.
          _bar.Baz.Wurgle();
          _bar.Baz.Slurgle();
        });
      }

      [Test]
      public void Ordered_calls_with_delegates()
      {
        var func = Substitute.For<Func<string>>();
        func();
        func();

        XReceived.Only(() =>
        {
          func();
          func();
        });
      }

      [Test]
      public void Non_matching_ordered_calls_with_delegates()
      {
        var func = Substitute.For<Action>();
        func();

        Assert.Throws<CallSequenceNotFoundException>(() =>
             XReceived.Only(() =>
             {
               func();
               func();
             })
            );
      }

      [Test]
      public void Event_subscription()
      {
        _foo.OnFoo += () => { };

        XReceived.Only(() =>
        {
          _foo.OnFoo += Arg.Any<Action>();
        });
      }


      public interface IFoo
      {
        void Start();
        void Start(int i);
        void Finish();
        void FunkyStuff(string s);
        event Action OnFoo;
      }

      public interface IBar
      {
        void Begin();
        void End();
        IBaz Baz { get; }
      }

      public interface IBaz
      {
        string Flurgle { get; set; }
        void Wurgle();
        void Slurgle();
      }
    }
  }

}
