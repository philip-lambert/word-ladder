using System;
using System.Linq;

using WordLadder.Lib.DictionaryLoader;

namespace WordLadder.Lib.DictionaryParser
{
  public class DictionaryParser : IDictionaryParser
  {
    private string _start;
    private string _end;
    private string[] _dictionary;

    public DictionaryParser(string start, string end, IDictionaryLoader loader)
    {
      if (string.IsNullOrWhiteSpace(start))
        throw new ArgumentNullException(nameof(start));
      if (string.IsNullOrWhiteSpace(end))
        throw new ArgumentNullException(nameof(end));
      if (loader == null)
        throw new ArgumentNullException(nameof(loader));

      _start = start;
      _end = end;
      _dictionary = loader.Load()
        .Where(obj => obj.IsValidWord()) // Strips out invalid words from the dictionary
        .ToArray();
    }

    public string[] Parse()
    {
      //TODO: DictionaryParser.Parse
      return Array.Empty<string>();
    }

    public static string[] Parse(string start, string end, IDictionaryLoader loader)
    {
      DictionaryParser parser = new DictionaryParser(start, end, loader);
      string[] result = parser.Parse();
      return result;
    }
  }
}
