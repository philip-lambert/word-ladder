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
* Removes any invalid words.
* Finds all possible paths from start to end.
* Returns the shortest path.

**TL;DR** This is all contained in [.\Lib\DictionaryParser\BFSDictionaryParser.cs](WordLadder/Lib/DictionaryParser/BFSDictionaryParser.cs).

## Project Structure
Ordinarily I'd structure this into three projects: -
* WordLadder (command line application) Simple executable which uses...
* WordLadder.Lib (class library) This is the where the actual logic lives
* WordLadder.Tests (class library) These unit tests validate the code in WordLadder.Lib

The logic code is in it's own class library so that it is completley de-coupled from the application. This reduces dependencies, making unit test creation easier.

The tests are in their own class library ensuring that test code is never included in a production build.

For the sake of simplicity I've kept all code in a single console application, and organised code into Lib/Tests folders.


## Performance
Invalid words are ignored when loading the dictionary. This reduces the example dictionary from **26880** entries to **2238**. I did consider ignoring words that have any lowercase letters, but this takes the number of example entries down to only 39 which is too small to be useful. Also I think it's a useful feature if the app is not case sensitive.


Breadth first vs djikstra?

## Techniques
I've added two extension methods on the string class: - IsValidWord and IsOneLetterDifferent.
Normally I wouldn't extend a basic data type such as string, but this is somewhat mitigated as they are marked internal. In my opinion these extensions do help with code readability as you can use: -
```
if (word1.IsOneLetterDifferent(word2)) ...
```

DictionaryLoaderFactory - Factory Pattern, further extension for web service/port reader/asynch etc
BaseDictionaryParser
TextFileDictionaryLoader/ZipFileDictionaryLoader **S**OLID Principle
Moq

## References
* [Word Ladder](https://www.geeksforgeeks.org/word-ladder-length-of-shortest-chain-to-reach-a-target-word/)
* [C# – Breadth First Search (BFS) using Queue](https://www.csharpstar.com/csharp-breadth-first-search/)

## Summary
![Shirly](https://static.boredpanda.com/blog/wp-content/uploads/2019/05/airplane-movie-funny-moments-fb15-png__700.jpg)
