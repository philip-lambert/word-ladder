using Microsoft.VisualStudio.TestTools.UnitTesting;

using WordLadder.Lib;

namespace WordLadder.Tests
{
  [TestClass]
  public class ExtensionsTests
  {
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("c")]
    [DataRow("cas")]
    [DataRow("c se")]
    [DataRow("c@se")]
    [DataRow("cases")]
    public void IsValidWord_ReturnsFalse_ForInvalidWord(string word)
    {
      bool actual = word.IsValidWord();
      Assert.IsFalse(actual);
    }

    [DataTestMethod]
    [DataRow("case")]
    [DataRow("Case")]
    public void IsValidWord_ReturnsTrue_ForValidWord(string word)
    {
      bool actual = word.IsValidWord();
      Assert.IsTrue(actual);
    }

    [DataTestMethod]
    [DataRow(null, "cast")]
    [DataRow("", "cast")]
    [DataRow("c", "cast")]
    [DataRow("cas", "cast")]
    [DataRow("c se", "cast")]
    [DataRow("c@se", "cast")]
    [DataRow("cases", "cast")]
    [DataRow("case", null)]
    [DataRow("case", "")]
    [DataRow("case", "c")]
    [DataRow("case", "ca")]
    [DataRow("case", "cas")]
    [DataRow("case", "c st")]
    [DataRow("case", "c@st")]
    [DataRow("case", "casts")]
    public void IsOneLetterDifferent_ReturnsFalse_ForInvalidWords(string word1, string word2)
    {
      bool actual = word1.IsOneLetterDifferent(word2);
      Assert.IsFalse(actual);
    }

    [DataTestMethod]
    [DataRow("case", "case")] // 0 letters different
    [DataRow("case", "lose")] // 2 letters different
    [DataRow("case", "lone")] // 3 letters different
    [DataRow("case", "long")] // 4 letters different
    [DataRow("case", "caet")] // 1 letter different, but in wrong order
    public void IsOneLetterDifferent_ReturnsFalse_ForNonAdjacentWords(string word1, string word2)
    {
      bool actual = word1.IsOneLetterDifferent(word2);
      Assert.IsFalse(actual);
    }

    [DataTestMethod]
    [DataRow("same", "came")]
    [DataRow("Came", "casE")]
    [DataRow("CASE", "cast")]
    [DataRow("cast", "COST")]
    public void IsOneLetterDifferent_ReturnsTrue_ForAdjacentWords(string word1, string word2)
    {
      bool actual = word1.IsOneLetterDifferent(word2);
      Assert.IsTrue(actual);
    }
  }
}
