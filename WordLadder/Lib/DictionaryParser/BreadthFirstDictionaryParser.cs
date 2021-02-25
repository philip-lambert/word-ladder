using System.Linq;
using System.Collections.Generic;

namespace WordLadder.Lib.DictionaryParser
{
  /// <summary>
  /// Class to perform a breadth first search in the dictionary to all find word ladders between start and end words
  /// </summary>
  public class BreadthFirstDictionaryParser : BaseDictionaryParser
  {
    public BreadthFirstDictionaryParser(string start, string end, DictionaryLoader.IDictionaryLoader loader)
      : base(start, end, loader)
    {
    }

    public override string[][] Parse()
    {
      List<string> dictionary = _dictionary.ToList();
      List<WordNode> results = new List<WordNode>();
      Queue<WordNode> queue = new Queue<WordNode>(new[] { new WordNode(_start, null) });

      while (queue.Count > 0)
      {
        WordNode current = queue.Dequeue();

        // Current word matches end word == complete path
        if (current.Word.Equals(_end, System.StringComparison.OrdinalIgnoreCase))
        {
          results.Add(current);
          continue;
        }

        // Current word is one letter different to end word == complete path
        if (current.Word.IsOneLetterDifferent(_end))
        {
          results.Add(new WordNode(_end, current));
          continue;
        }

        // Find more words to that are one letter different and add into queue for processing
        for (int loop = dictionary.Count - 1; loop >= 0; loop--)
        {
          string word = dictionary[loop];
          if (word.IsOneLetterDifferent(current.Word))
          {
            dictionary.RemoveAt(loop);
            queue.Enqueue(new WordNode(word, current));
          }
        }
      }

      string[][] result = results
        .Select(obj => obj.GetPath()) // Get all paths
        .GroupBy(obj => obj.Length)   // Group by path length
        .OrderBy(obj => obj.Key)      // Order by path length
        .First()                      // Get first (shortest paths) group
        .ToArray();                   // Return shortest paths
      return result;
    }
  }
}
