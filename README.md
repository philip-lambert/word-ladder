# Word Ladder
.NET 5.0 command line application written using Visual Studio 2019.
```
WordLadder.exe /?
WordLadder.exe /help
WordLadder.exe STARTWORD ENDWORD dictionaryFile outputFile
WordLadder.exe /start STARTWORD /end ENDWORD /dictionary dictionaryFile /output outputFile
```

## Project Structure
Ordinarily I'd structure this into three projects: -
* WordLadder (command line application) Simple executable which uses...
* WordLadder.Lib (class library) This is the where the actual logic lives
* WordLadder.Tests (class library) These unit tests validate the code in WordLadder.Lib

The logic code is in it's own class library so that it is completley de-coupled from the application, reducing dependencies which complicates unit testing. The tests are in their own class library ensuring that test code is never included in a production build.

For the sake of simplicity I've kept all code in a single console application, and organised code into Lib/Tests folders.

## Assumptions
From the example start/end words I've made the following assumptions. A word is invalid if: -
* It is not exactly 4 characters long
* It contains any spaces
* It contains any non-alphabetic characters

I'm also assuming that the start and end words don't necessarily have to be in the dictionary.

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
