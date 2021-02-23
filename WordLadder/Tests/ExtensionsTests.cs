using Microsoft.VisualStudio.TestTools.UnitTesting;

using WordLadder.Lib;

namespace WordLadder.Tests
{
  [TestClass]
  public class ExtensionsTests
  {
    [DataTestMethod]
    // IsValid should return false
    [DataRow(null, false)]
    [DataRow("", false)]
    [DataRow("c", false)]
    [DataRow("cas", false)]
    [DataRow("cases", false)]
    [DataRow("c@se", false)]
    // IsValid should return true
    [DataRow("case", true)]
    [DataRow("Case", true)]
    public void IsValid_ReturnsExpected_ForInputs(string str, bool expected)
    {
      bool actual = str.IsValid();
      Assert.AreEqual(expected, actual);
    }

    [DataTestMethod]
    // IsOneLetterDifferent should return false
    [DataRow(null, "cast", false, false)]
    [DataRow("", "cast", false, false)]
    [DataRow("c", "cast", false, false)]
    [DataRow("cas", "cast", false, false)]
    [DataRow("cases", "cast", false, false)]
    [DataRow("c@se", "cast", false, false)]
    [DataRow("case", null, false, false)]
    [DataRow("case", "", false, false)]
    [DataRow("case", "", false, false)]
    [DataRow("case", "cas", false, false)]
    [DataRow("case", "castle", false, false)]
    [DataRow("case", "c@st", false, false)]
    [DataRow("case", "CAST", true, false)]
    [DataRow("CASE", "cast", true, false)]
    [DataRow("case", "vast", false, false)]
    [DataRow("same", "cost", false, false)]
    [DataRow("tsac", "cast", false, false)]
    [DataRow("case", "cats", false, false)]
    // IsOneLetterDifferent should return true
    [DataRow("case", "cast", false, true)]
    [DataRow("CASE", "cast", false, true)]
    [DataRow("cast", "case", false, true)]
    [DataRow("cast", "CASE", false, true)]
    public void IsOneLetterDifferent_ReturnsExpected_ForInputs(string str1, string str2, bool caseSensitive, bool expected)
    {
      bool actual = str1.IsOneLetterDifferent(str2, caseSensitive);
      Assert.AreEqual(expected, actual);
    }
  }
}
