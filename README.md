
[![Build status](https://ci.appveyor.com/api/projects/status/cuik88kcuy9qbr2n?svg=true)](https://ci.appveyor.com/project/grzesiek-galezowski/tdd-toolkit)
[![NuGet version](https://badge.fury.io/nu/tdd-toolkit.svg)](http://badge.fury.io/nu/tdd-toolkit)
![](https://reposs.herokuapp.com/?path=grzesiek-galezowski/tdd-toolkit&style=flat)
[![License](http://img.shields.io/:license-mit-blue.svg)](http://doge.mit-license.org)

tdd-toolkit 
===========

Toolkit used in Test-Driven Development ebook tutorial: github.com/grzesiek-galezowski/tdd-ebook.



The idea of "Any" class is taken from guys at www.sustainabletdd.com

Note: this project does not aim to implement a general-purpose tool with extensibility points, rather, it's an example to start working from when creating your own customized wrapper. If you need hardcore extensibility, go straight to https://github.com/AutoFixture and grab the general purpose tool that has possibilities far beyond generating anonymous data.

Implemented generation methods:
-

Numbers
-

A distinct integer value:  

    Any.Integer()
    
A distinct double value:  
    
    Any.Double()
    
A distinct long integer value:  
    
    Any.LongInteger()
    
A distinct short integer value:      
    
    Any.ShortInteger()
    
A distinct integer value different than any of the passed values:  
    
    Any.IntegerOtherThan(params int[] excluded)
    
A distinct byte value:
    
    Any.Byte()
    
A distinct byte value different than any of the passed values:      
    
    Any.ByteOtherThan(params byte[] others)

Strings & chars
-

A distinct string:

    Any.String()
    
A string matching a regex (only a small subset of features is supported - you'll have ti try and see if it fits you)
    
    Any.StringMatching(string pattern)
    
A distinct string of a given length:
    
    Any.StringOfLength(int charactersCount)
    
A distinct string other that all passed strings (may be a subset or superset though):
    
    Any.StringOtherThan(params string[] alreadyUsedStrings)
    
A distinct string that does not contain any of given strings:
    
    Any.StringNotContaining(params string[] excludedSubstrings)
    
A distinct string that contains a given string:
    
    Any.StringContaining(string str)
    
A distinct alpha-numeric string:
    
    Any.AlphaString()
    
A distinct alpha-string (latin characters only, no regional characters):
    
    Any.AlphaString(int maxLength)
    
A distinct string matching Identifier requirements (first character is letter, the rest is alphanumeric):
    
    Any.Identifier()
    
A single distinct letter character:
    
    Any.AlphaChar()
    
A single distinct digit character:
    
    Any.DigitChar()

Enums
-

A distinct value from all in an enum (of course, for single-value enums will always return the same value :-)):

    Any.Of<T>() where T : struct, IConvertible
    
A distinct value from all in enum besides the passed one (if all-but-one values are specified, always returns the same value. If all values are specified, throws exception):
    
    Any.Besides<T>(params T[] excludedValues) where T : struct, IConvertible
    
Collections
-

Sorted Set:

    Any.SortedSet<T>()
    
Enumerable of objects of type T:
    
    Any.Enumerable<T>()
    
Enumerable of objects of type T different than passed excluded ones:

    Any.EnumerableWithout<T>(params T[] excluded)
    
Array of objects of type T:

    Any.Array<T>()
    
Array of objects of type T different than passed excluded ones:
    
    Any.ArrayWithout<T>(params T[] excluded)
    
List of objects of type T:
    
    Any.List<T> List<T>()
    
Set of objects of type T:
    
    Any.Set<T>()
    
Set of objects of type TKey, TValue:    
    
    Any.Dictionary<TKey, TValue>()
    
Enumerable of objects of type T sorted descending:
    
    Any.EnumerableSortedDescending<T>()

Others
-

A main generic generation method, capable of generating both primitive and reference types:

    Any.Instance<T>()

Picks any element from collection:
    
    Any.From<T>(params T[] possibleValues)
    
DateTime:

    Any.DateTime()
    
TimeSpan:

    Any.TimeSpan()
    
Generic generation method for values only (subset of Any.Instance<T>()):  

    Any.ValueOf<T>()
    Any.Value<T>()

Like above, but makes sure none of the passed values will not get returned:

    Any.ValueOtherThan<T>(params T[] omittedValues)

For booleans:    
    
    Any.Boolean()
    
For objects (useful e.g. for synchronization roots):    
    
    Any.Object()

For exceptions:

    Any.Exception()

For Type and MethodInfo:

    Any.Method()
    Any.Type()

Legal XML tag name:

    Any.LegalXmlTagName()
    
Exploding type (any overridable method called on created instance throws an exception) - useful for specifying null objects:
    
    Any.Exploding<T>() where T : class
    
Generic generation method for objects only (subset of Any.Instance<T>()):      
    
    Any.InstanceOf<T>()
    Any.Instance<T>()
    
URI object:
    
    Any.Uri()
    
URL string:
    
    Any.UrlString()
    
Port number:
    
    Any.Port()
    
string containing IP address:

    Any.Ip()

[![](https://codescene.io/projects/562/status.svg) Get more details at **codescene.io**.](https://codescene.io/projects/562/jobs/latest-successful/results)
