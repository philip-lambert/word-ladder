using System;
using System.Linq;

namespace WordLadder.Lib
{
  internal static class StringExtensions
  {
    private const int MAX_WORD_LENGTH = 4;
    private static readonly char[] VALID_WORD_CHARS = Enumerable.Range('a', 26)
      .Concat(Enumerable.Range('A', 26))
      .Select(obj => Convert.ToChar(obj))
      .ToArray();

    /// <summary>
    /// Returns true if the supplied word is valid
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    internal static bool IsValidWord(this string word)
    {
      if (word == null)
        return false;

      word = word.Replace(" ", string.Empty);

      if (word.Length != MAX_WORD_LENGTH)
        return false;

      if (word.Except(VALID_WORD_CHARS).Any())
        return false;

      return true;
    }

    /// <summary>
    /// Returns true if the supplied words are exactly one letter different
    /// </summary>
    /// <param name="word1"></param>
    /// <param name="word2"></param>
    /// <returns></returns>
    internal static bool IsOneLetterDifferent(this string word1, string word2)
    {
      if (!word1.IsValidWord() || !word2.IsValidWord())
        return false;

      word1 = word1.ToUpper();
      word2 = word2.ToUpper();

      int difference = 0;
      for (int loop = 0; loop < word1.Length; loop++)
        if (word1[loop] != word2[loop])
          difference++;

      bool result = difference == 1;
      return result;
    }
  }
}
