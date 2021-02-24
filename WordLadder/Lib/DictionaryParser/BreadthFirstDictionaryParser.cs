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
        WordNode result = null;

        // Current word matches end word
        if (current.Word.Equals(_end, System.StringComparison.OrdinalIgnoreCase))
          result = current;

        // Current word is one letter different to end word
        if (current.Word.IsOneLetterDifferent(_end))
          result = new WordNode(_end, current);

        if (result != null)
        {
          int resultsCount = results.Count;
          int firstPathLength = resultsCount > 0 ? results[0].GetPath().Length : 0;
          int resultPathLength = result.GetPath().Length;

          bool clearResults = resultsCount > 0 && firstPathLength > resultPathLength;
          if (clearResults)
            results.Clear();

          bool addResult = resultsCount == 0 || firstPathLength == resultPathLength;
          if (addResult)
            results.Add(result);

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

      return results.Select(obj => obj.GetPath()).ToArray();
    }
  }
}
