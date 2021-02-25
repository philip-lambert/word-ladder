# Word Ladder
.NET 5.0 command line application written using Visual Studio 2019.
```
WordLadder.exe /?
WordLadder.exe /help
WordLadder.exe startWord endWord dictionaryFile outputFile
WordLadder.exe /start startWord /end endWord /dictionary dictionaryFile /output outputFile
```

## Research
After a quick google for "word ladder code" I found a few examples which use the [breadth first search](https://en.wikipedia.org/wiki/Breadth-first_search) algorithm. This is based on the shortest path first algorithm (a.k.a [Dijkstra's](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm)), which would be the better choice if we had to find words with two or more different letters.

## Assumptions
* Start and end words don't necessarily have to be present in the dictionary.
* Returning more than one path is valid as long as they have the same length. I.e. if the shortest path is 6 steps, it's fine to return multiple paths as long as they're all 6 steps.

From the example start/end words I'm also assuming that a word is valid if: -
* It is exactly 4 characters long.
* It has no spaces.
* It has no non-alphabetic characters.

## Design
At it's most basic, we need a class that: -
* Accepts start, end and dictionary parameters.
* Removes any invalid words from the dictionary (hopefully making the search faster).
* Finds all possible paths from start to end.
* Returns the shortest path.

**TL;DR** This is all contained in [BFSDictionaryParser.cs](WordLadder/Lib/DictionaryParser/BFSDictionaryParser.cs).

## Project Structure
Ordinarily I'd structure this into three projects: -
* WordLadder.Cmd - Command line application which uses...
* WordLadder.Lib - A class library where the actual logic lives.
* WordLadder.Tests - A class library to contain unit tests.

I put the logic code is in it's own class library to de-couple it from the presentation layer. This makes the code reusable accross multiple applications, as well as reducing dependencies which makes unit test creation easier. Putting the tests in their own class library ensuring that test code is never included in a production build.

For the sake of simplicity I've kept all code in a single console application, and organised code into Lib and Tests folders. I also installed MSTest and Moq via NuGet.

## Approach
The application will rely on two main functions. One which returns true if a word is valid, and another that returns true if two words are exactly one letter different. I decided to create these as extension methods on the string class, which I normally wouldn't do on a basic data type. However they are internal methods and in my opinion they do help with code readability: -
```
if (str.IsValidWord()) ...

if (str1.IsOneLetterDifferent(str2)) ...
```
My first step was to create [ExtensionsTests.cs](WordLadder/Tests/ExtensionsTests.cs), then [Extensions.cs](WordLadder/Lib/Extensions.cs).

Next I needed to a function that would return the shortest paths using BFS (see [BFSDictionaryParserTests.cs](WordLadder/Tests/DictionaryParser/BFSDictionaryParserTests.cs) and [BFSDictionaryParser.cs](WordLadder/Lib/DictionaryParser/BFSDictionaryParser.cs)). The parser has a base class ([BaseDictionaryParser.cs](WordLadder/Lib/DictionaryParser/BaseDictionaryParser.cs)) which validates the inputs and strips out any invalid words from the dictionary array (which reduces the example dictionary from **26880** entries to **2238**). If the requirement changed to words with 2 or more matching letters we could derive a SPFDictionaryParser class to cater for this. Any methods which use our parser could be supplied it as a BaseDictionaryParser, or as an [IDictionaryParser.cs](WordLadder/Lib/DictionaryParser/IDictionaryParser.cs) (which could be Moq'd for unit tests).

## Performance
Invalid words are ignored when loading the dictionary.  I did consider ignoring words that have any lowercase letters, but this takes the number of example entries down to only 39 which is too small to be useful. Also I think it's a useful feature if the app is not case sensitive.


Breadth first vs djikstra?

## Techniques
I've added two extension methods on the string class: - IsValidWord and IsOneLetterDifferent.


DictionaryLoaderFactory - Factory Pattern, further extension for web service/port reader/asynch etc
BaseDictionaryParser
TextFileDictionaryLoader/ZipFileDictionaryLoader **S**OLID Principle
Moq

## References
* [Word Ladder](https://www.geeksforgeeks.org/word-ladder-length-of-shortest-chain-to-reach-a-target-word/)
* [C# – Breadth First Search (BFS) using Queue](https://www.csharpstar.com/csharp-breadth-first-search/)

## Summary
![Shirly](https://static.boredpanda.com/blog/wp-content/uploads/2019/05/airplane-movie-funny-moments-fb15-png__700.jpg)
