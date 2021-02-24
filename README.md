# word-ladder
.NET 5.0 command line application written using Visual Studio 2019.

The application can be run using: -
## Usage
Markup :  `code()`
WordLadder.exe /?
WordLadder.exe /help
WordLadder.exe startWord endWord dictionaryFile outputFile
WordLadder.exe /start startWord /end endWord /dictionary dictionaryFile /output outputFile

## Assumptions
The challenge states the start and end words are four-letter long. So with regards to words (both input and dictionary) I'm making the following assumptions: -
* A valid word must be exactly 4 letters long
* A valid word cannot contain any non-alphabetic characters (the challenge states 'letters')
* A valid word cannot contain any spaces (that would constitute more than one word)
So when loading the dictionary invalid words are ignored. This reduces the example dictionary from 26880 entries to 2238.

text file/zip file
BaseDictionaryLoader - Liskov Substitution

## Project Structure
Ordinarily I'd structure this into three or more projects: -
* WordLadder.Cmd (command line application). Simple executable which uses...
* WordLadder.Lib (class library). This is the where the actual logic lives.
* WordLadder.Tests (class library). These unit tests validate the code in WordLadder.Lib.

The reason the tests are in a separate library is so that test code is never packaged up with production code.
The reason the logic is in a separate library is so that it is totally decoupled from the application/ui logic (can be used in a cmd line/web app/winforms/etc).

For the sake of simplicity I've kept all code in a single .NET 5.0 console application, and organised code into Lib/Tests folders.

## Techniques
I've added two extension methods on the string class: - IsValid and IsOneLetterDifferent.
Normally I'd consider it somewhat dangerous to extend a basic data type such as string, but in this case it does help with code readability.
Case sensitive option


Input/Dictionary words are only valid if: -
* They are 4 characters long.
* They only contain alpha (a-z) characters.




