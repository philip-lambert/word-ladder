# word-ladder
.NET 5.0 command line application written using Visual Studio 2019.

## Usage
```
WordLadder.exe /?
WordLadder.exe /help
WordLadder.exe startWord endWord dictionaryFile outputFile
WordLadder.exe /start startWord /end endWord /dictionary dictionaryFile /output outputFile
```

## Assumptions
The challenge states the start and end words are four-letters long. So in addition to length I'm making the following assumptions (for both input and dictionary words): -
* A valid word cannot contain any non-alphabetic characters. I'm assuming this as the challenge states 'letters'.
* A valid word cannot contain any spaces (which would constitute more than one word).

I'm also assuming that the start and end words don't necessarily have to be in the dictionary.

## Project Structure
Ordinarily I'd structure this into three or more projects: -
* WordLadder.Cmd (command line application). Simple executable which uses...
* WordLadder.Lib (class library). This is the where the actual logic lives.
* WordLadder.Tests (class library). These unit tests validate the code in WordLadder.Lib.

The reason the tests are in a separate library is so that test code is never packaged up with production code.
The reason the logic is in a separate library is so that it is totally decoupled from the application/ui logic (can be used in a cmd line/web app/winforms/etc).

For the sake of simplicity I've kept all code in a single console application, and organised code into Lib/Tests folders.

## Performance
When loading the dictionary invalid words are ignored. This reduces the example dictionary from **26880** entries to **2238**. I did consider ignoring words that aren't 100% uppercase, but this takes the number of entries down to only 39. Also I think it's a useful feature if the app is not case sensitive.
Breadth first vs djikstra?

## Techniques
I've added two extension methods on the string class: - IsValidWord and IsOneLetterDifferent.
Normally I wouldn't extend a basic data type such as string, but this is somewhat mitigated as they are marked internal. In my opinion these extensions do help with code readability as you can use: -
```
if (word1.IsOneLetterDifferent(word2)) ...
```

DictionaryLoaderFactory - Factory Pattern, further extension for web service/port reader/asynch etc
TextFileDictionaryLoader/ZipFileDictionaryLoader **S**OLID Principle
Add Moq
