using System;
using System.Linq;
using System.Collections.Generic;

namespace WordLadder.Lib.DictionaryParser
{
  /// <summary>
  /// Class returned from <see cref="DictionaryParser.Parse"/>
  /// </summary>
  public class ParseResult
  {
    private List<WordNode> _wordNodes = new List<WordNode>();

    public void Add(WordNode wordNode)
    {
      if (wordNode == null)
        throw new ArgumentNullException(nameof(wordNode));

      _wordNodes.Add(wordNode);
    }

    public string[][] GetShortestPaths()
    {
      var paths = _wordNodes.Select(obj => obj.GetPath());
      var groups = paths.GroupBy(obj => obj.Length);
      var ordered = groups.OrderBy(obj => obj.Key);
      var first = ordered.First();
      var result = first.ToArray();
      return result;
    }
  }
}
