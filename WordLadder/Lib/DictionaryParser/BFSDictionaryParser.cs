using System;
using System.Linq;
using System.Collections.Generic;

namespace WordLadder.Lib.DictionaryParser
{
  /// <summary>
  /// Performs a breadth first search in the dictionary to find shortest word ladder(s) between start and end words
  /// </summary>
  public class BFSDictionaryParser : BaseDictionaryParser
  {
    private class Node
    {
      private readonly Node _parent;

      public string Word { get; private set; }
      public string[] Path => GetPath();

      internal Node(string word, Node parent)
      {
        if (string.IsNullOrWhiteSpace(word))
          throw new ArgumentNullException(nameof(word));

        Word = word;
        _parent = parent;
      }

      public override string ToString()
      {
        return string.Join(" - ", GetPath());
      }

      private string[] GetPath()
      {
        List<string> result = new List<string>();
        if (_parent != null)
          result.AddRange(_parent.GetPath());
        result.Add(Word);
        return result.ToArray();
      }
    }

    public BFSDictionaryParser(string start, string end, DictionaryLoader.IDictionaryLoader loader)
      : base(start, end, loader)
    {
    }

    public override string[][] Parse()
    {
      List<string> dictionary = _dictionary.ToList();
      List<Node> results = new List<Node>();
      Queue<Node> queue = new Queue<Node>(new[] { new Node(_start, null) });

      while (queue.Count > 0)
      {
        Node current = queue.Dequeue();

        // Current word matches end word == complete path
        if (current.Word.Equals(_end, StringComparison.OrdinalIgnoreCase))
        {
          results.Add(current);
          continue;
        }

        // Current word is one letter different to end word == complete path
        if (current.Word.IsOneLetterDifferent(_end))
        {
          results.Add(new Node(_end, current));
          continue;
        }

        // Find more words to that are one letter different and add into queue for processing
        for (int loop = dictionary.Count - 1; loop >= 0; loop--)
        {
          string word = dictionary[loop];
          if (word.IsOneLetterDifferent(current.Word))
          {
            dictionary.RemoveAt(loop);
            queue.Enqueue(new Node(word, current));
          }
        }
      }

      string[][] result = results
        .Select(obj => obj.Path)    // Get all paths
        .GroupBy(obj => obj.Length) // Group by path length
        .OrderBy(obj => obj.Key)    // Order by path length
        .First()                    // Get first (shortest paths) group
        .ToArray();                 // Return shortest paths
      return result;
    }
  }
}
