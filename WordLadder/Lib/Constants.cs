using System;
using System.Linq;

namespace WordLadder.Lib
{
  public class Constants
  {
    public const int MAX_WORD_LENGTH = 4;
    public static readonly char[] VALID_WORD_CHARS = Enumerable.Range('a', 26)
      .Concat(Enumerable.Range('A', 26))
      .Select(obj => Convert.ToChar(obj))
      .ToArray();
  }
}
