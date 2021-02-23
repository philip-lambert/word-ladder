using System;
using System.Linq;

namespace WordLadder.Lib
{
  internal static class Extensions
  {
    internal static bool IsValid(this string str)
    {
      if (str == null)
        return false;

      str = str.Trim();

      if (str.Length != Constants.MAX_WORD_LENGTH)
        return false;

      if (str.Except(Constants.VALID_WORD_CHARS).Any())
        return false;

      return true;
    }

    internal static bool IsOneLetterDifferent(this string str1, string str2, bool caseSensitive = false)
    {
      if (!str1.IsValid() || !str2.IsValid())
        return false;

      if (!caseSensitive)
      {
        str1 = str1.ToUpper();
        str2 = str2.ToUpper();
      }

      int difference = 0;
      for (int loop = 0; loop < str1.Length; loop++)
        if (str1[loop] != str2[loop])
          difference++;
      bool result = difference == 1;
      return result;
    }
  }
}
