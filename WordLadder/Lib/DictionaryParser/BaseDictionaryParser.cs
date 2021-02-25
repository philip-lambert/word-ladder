using System;
using System.Linq;
using System.Collections.Generic;

using WordLadder.Lib.DictionaryLoader;

namespace WordLadder.Lib.DictionaryParser
{
  public abstract class BaseDictionaryParser : IDictionaryParser
  {
    protected readonly string _start;
    protected readonly string _end;
    protected readonly string[] _dictionary;

    public BaseDictionaryParser(string start, string end, IDictionaryLoader loader)
      : this(start, end, loader.Load())
    {
    }

    public BaseDictionaryParser(string start, string end, IEnumerable<string> dictionary)
    {
      if (!start.IsValidWord())
        throw new ArgumentException("Invalid start word", nameof(start));
      if (!end.IsValidWord())
        throw new ArgumentException("Invalid end word", nameof(end));
      if (dictionary == null)
        throw new ArgumentNullException(nameof(dictionary));

      _start = start;
      _end = end;
      _dictionary = dictionary
        .Where(obj => obj.IsValidWord())  // Strips out invalid words
        .Distinct()                       // Strips out repeated words
        .ToArray();

      if (_dictionary.Length == 0)
        throw new ArgumentException("Dictionary contains no valid words", nameof(dictionary));
    }

    public abstract string[][] Parse();
  }
}
