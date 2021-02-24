using System;
using System.Collections.Generic;
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

    public ParseResult Parse()
    {
      ParseResult result = new ParseResult();

      List<string> dictionary = _dictionary.ToList();
      Queue<WordNode> queue = new Queue<WordNode>(new[] { new WordNode(_start, null) });
      while (queue.Count > 0)
      {
        WordNode wordNode = queue.Dequeue();

        // Current word matches end word
        if (wordNode.Word.Equals(_end, StringComparison.OrdinalIgnoreCase))
        {
          result.Add(wordNode);
          continue;
        }

        // Current word is one letter different to end word
        if (wordNode.Word.IsOneLetterDifferent(_end))
        {
          result.Add(new WordNode(_end, wordNode));
          continue;
        }

        // Add more "similar" words to queue for processing
        for (int loop = dictionary.Count - 1; loop >= 0; loop--)
        {
          string word = dictionary[loop];
          if (word.IsOneLetterDifferent(wordNode.Word))
          {
            dictionary.RemoveAt(loop);
            queue.Enqueue(new WordNode(word, wordNode));
          }
        }
      }

      return result;
    }
  }
}
