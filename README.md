# Word Ladder
.NET 5.0 command line application written using Visual Studio 2019 for Blue Prism technical test.
```
WordLadder.exe /?
WordLadder.exe /help
WordLadder.exe startWord endWord dictionaryFile outputFile
WordLadder.exe /start startWord /end endWord /dictionary dictionaryFile /output outputFile
```

## Research
After a quick google for "word ladder code" I found a few examples which use the [breadth first search](https://en.wikipedia.org/wiki/Breadth-first_search) algorithm. This is based on the shortest path first algorithm (a.k.a [Dijkstra's](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm)).

## Assumptions
* Start and end words don't necessarily have to be present in the dictionary.
* Returning more than one path is valid as long as they have the same length. I.e., if the shortest path is 6 steps, it's fine to return multiple paths as long as they're all 6 steps.

From the example start and end words supplied I'm also assuming that a word is valid if: -
* It is exactly 4 characters long.
* It has no spaces.
* It has no non-alphabetic characters.

## Design
At its most basic, we need a class that: -
* Accepts start, end and dictionary array parameters.
* Removes any invalid words from the dictionary (hopefully making the search faster).
* Finds all possible paths from start to end.
* Returns the shortest path.

**TL;DR** This is all contained in [BFSDictionaryParser.cs](WordLadder/Lib/DictionaryParser/BFSDictionaryParser.cs) (plus its parent class).

## Project Structure
Ordinarily I'd structure this into three projects: -
* WordLadder.Cmd - Command line application which uses...
* WordLadder.Lib - A class library where the actual logic lives.
* WordLadder.Tests - A class library to contain unit tests.

I put logic code is in its own class library to de-couple from the presentation layer. This makes the code reusable across multiple applications, as well as reducing dependencies (making unit test creation easier). Putting the tests in their own class library ensuring that test code is never included in a production build.

For the sake of simplicity, I've kept all code in a single console application, organised into Lib and Tests folders. I also installed MSTest and Moq via NuGet.

## Approach
The application will be built on two main functions. One which returns true if a word is valid, and another that returns true if two words are exactly one letter different. I decided to create these as extension methods on the string class, which I normally wouldn't do on a basic data type. However, they are internal methods and in my opinion they do help with code readability: -
```
if (str.IsValidWord()) ...

if (str1.IsOneLetterDifferent(str2)) ...
```
My first step was to create [StringExtensionsTests.cs](WordLadder/Tests/StringExtensionsTests.cs), then [StringExtensions.cs](WordLadder/Lib/StringExtensions.cs).

Next I needed to a function that would return the shortest paths using BFS (see [BFSDictionaryParserTests.cs](WordLadder/Tests/DictionaryParser/BFSDictionaryParserTests.cs) and [BFSDictionaryParser.cs](WordLadder/Lib/DictionaryParser/BFSDictionaryParser.cs)). The parser has a base abstract class ([BaseDictionaryParser.cs](WordLadder/Lib/DictionaryParser/BaseDictionaryParser.cs)) which validates the inputs and strips out any invalid words from the dictionary  (which reduces the example dictionary from **26880** entries to **2238**). Any applications requiring a parser could then be supplied it as a [BaseDictionaryParser.cs](WordLadder/Lib/DictionaryParser/BaseDictionaryParser.cs), or a [IDictionaryParser.cs](WordLadder/Lib/DictionaryParser/IDictionaryParser.cs) (which could be Moq'd for unit tests). If the requirement changed to words with 2 or more matching letters, we could derive a SPFDictionaryParser class (plus another extension method) to cater for this.

Finally, I need a function to load the supplied dictionary file. This can be done as simply as System.IO.File.ReadAllLines, but in aid of bonus points I made: -
* [IDictionaryLoader.cs](WordLadder/Lib/DictionaryFactory/IDictionaryLoader.cs) - Dictionary loader interface.
* [TextFileDictionaryLoader.cs](WordLadder/Lib/DictionaryFactory/TextFileDictionaryLoader.cs) - Loads from a text file.
* [ZipFileDictionaryLoader.cs](WordLadder/Lib/DictionaryFactory/ZipFileDictionaryLoader.cs) - Loads from a zip file.
* [DictionaryLoaderFactory.cs](WordLadder/Lib/DictionaryFactory/DictionaryLoaderFactory.cs) - Creates the appropriate dictionary loader based on the file extension.
My thinking is that we could handle future request to load from json/xml/database/web service/etc.

### More showing off
I added some code to verify the command line args and output help/errors if invalid (see [Program.cs](WordLadder/Program.cs) and [InputArgs.cs](WordLadder/InputArgs.cs)).

## Summary
![Shirly](https://static.boredpanda.com/blog/wp-content/uploads/2019/05/airplane-movie-funny-moments-fb15-png__700.jpg)
