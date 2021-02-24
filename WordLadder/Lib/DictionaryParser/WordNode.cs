﻿using System;
using System.Collections.Generic;

namespace WordLadder.Lib.DictionaryParser
{
  /// <summary>
  /// Class used when running <see cref="BreadthFirstDictionaryParser.Parse"/>
  /// </summary>
  public class WordNode
  {
    private readonly WordNode _parent;

    public string Word { get; private set; }
    public int Length => GetPath().Length;

    internal WordNode(string word, WordNode parent)
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

    public string[] GetPath()
    {
      List<string> result = new List<string>();
      if (_parent != null)
        result.AddRange(_parent.GetPath());
      result.Add(Word);
      return result.ToArray();
    }
  }
}
