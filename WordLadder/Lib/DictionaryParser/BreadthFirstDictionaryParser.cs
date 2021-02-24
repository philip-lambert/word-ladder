using System;
using System.Linq;
using System.Collections.Generic;

namespace WordLadder.Lib.DictionaryParser
{
  public class BreadthFirstDictionaryParser : BaseDictionaryParser
  {
    public BreadthFirstDictionaryParser(string start, string end, DictionaryLoader.IDictionaryLoader loader)
      : base(start, end, loader)
    {
    }

    /// <summary>
    /// Performs a breadth first search in the dictionary to all find word ladders between start and end words
    /// </summary>
    /// <returns></returns>
    public override ParseResult Parse()
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
