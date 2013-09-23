tdd-toolkit
===========

Toolkit used in Test-Driven Development ebook tutorial: github.com/grzesiek-galezowski/tdd-ebook.

The idea of "Any" class is taken from guys at www.sustainabletdd.com

Note: this project does not aim to implement a general-purpose tool with extensibility points, rather, it's an example to start working from when creating your own customized wrapper. If you need hardcore extensibility, go straight to https://github.com/AutoFixture and grab the general purpose tool that has possibilities far beyond generating anonymous data.

Implemented generation methods:
-

Numbers
-

    Any.Integer()
    Any.Double()
    Any.LongInteger()
    Any.ShortInteger()
    Any.IntegerOtherThan(params int[] excluded)
    Any.Byte()
    Any.ByteOtherThan(params byte[] others)

Strings & chars
-

    Any.String()
    Any.StringMatching(string pattern)
    Any.StringOfLength(int charactersCount)
    Any.StringOtherThan(params string[] alreadyUsedStrings)
    Any.StringNotContaining(params string[] excludedSubstrings)
    Any.StringContaining(string str)
    Any.AlphaString()
    Any.AlphaString(int maxLength)
    Any.Identifier()
    Any.AlphaChar()
    Any.DigitChar()

Enums
-

    Any.Of<T>() where T : struct, IConvertible
    Any.Besides<T>(params T[] excludedValues) where T : struct, IConvertible
    
Collections
-

    Any.SortedSet<T>()
    Any.EnumerableOfDerivativesFrom<T>() where T : class
    Any.ListOfDerivativesFrom<T>() where T : class
    Any.IEnumerable<T> Enumerable<T>()
    Any.EnumerableWithout<T>(params T[] excluded) where T : class
    Any.Array<T>()
    Any.ArrayWithout<T>(params T[] excluded) where T : class
    Any.List<T> List<T>()
    Any.Set<T>()
    Any.Dictionary<TKey, TValue>()
    Any.EnumerableSortedDescending<T>()

