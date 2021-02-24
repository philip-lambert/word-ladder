using System;
using System.Linq;

using WordLadder.Lib.DictionaryLoader;

namespace WordLadder.Lib.DictionaryParser
{
  public abstract class BaseDictionaryParser : IDictionaryParser
  {
    protected readonly string _start;
    protected readonly string _end;
    protected readonly string[] _dictionary;

    public BaseDictionaryParser(string start, string end, IDictionaryLoader loader)
    {
      if (!start.IsValidWord())
        throw new ArgumentException("Invalid start word", nameof(start));
      if (!end.IsValidWord())
        throw new ArgumentException("Invalid end word", nameof(end));
      if (loader == null)
        throw new ArgumentNullException(nameof(loader));

      _start = start;
      _end = end;
      _dictionary = loader.Load()
        .Where(obj => obj.IsValidWord()) // Strips out invalid words from the dictionary
        .ToArray();
      if (_dictionary.Length == 0)
        throw new ArgumentException("Dictionary contains no valid words", nameof(loader));
    }

    public abstract string[][] Parse();
  }
}
