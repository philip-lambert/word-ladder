# word-ladder
.NET 5.0 command line application written using Visual Studio 2019.
```
WordLadder.exe /?
WordLadder.exe /help
WordLadder.exe startWord endWord dictionaryFile outputFile
WordLadder.exe /start startWord /end endWord /dictionary dictionaryFile /output outputFile
```

## Project Structure
Ordinarily I'd structure this into three projects: -
* WordLadder **(command line application)** _Simple executable which uses..._
* WordLadder.Lib **(class library)** _This is the where the actual logic lives_
* WordLadder.Tests **(class library)** _These unit tests validate the code in WordLadder.Lib._

The logic code is in it's own class library so that it is completley de-coupled from the application, reducing dependencies which complicates unit testing. The tests are in their own class library ensuring that test code is never included in a production build.

For the sake of simplicity I've kept all code in a single console application, and organised code into Lib/Tests folders.

## Assumptions
The challenge states the start and end words are four letters long. So when loading the dictionary I'm making following assumptions: -
* Words that are not exactly four letters long are not valid and can be ignored.
* Words with a space characters are not valid and can be ignored.
* Words with non-alphabetic characters are not valid and can be ignored.

I'm also assuming that the start and end words don't necessarily have to be in the dictionary.

## Performance
Invalid words are ignored when loading the dictionary. This reduces the example dictionary from **26880** entries to **2238**. I did consider ignoring words that have lowercase letters, but this takes the number of example entries down to only 39. Also I think it's a useful feature if the app is not case sensitive.
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

![Shirly](https://static.boredpanda.com/blog/wp-content/uploads/2019/05/airplane-movie-funny-moments-fb15-png__700.jpg)