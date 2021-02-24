using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WordLadder.Lib.DictionaryLoader;
using WordLadder.Lib.DictionaryParser;

namespace WordLadder.Tests.DictionaryParser
{
  [TestClass]
  public class BreadthFirstDictionaryParserTests
  {
    [DataTestMethod]
    [DataRow("same", "cost", new[] { "cast", "case", "came" }, new[] { "same", "came", "case", "cast", "cost" })]
    [DataRow("ABCV", "EBAD", new[] { "ABCD", "EBAD", "EBCD", "XYZA" }, new[] { "ABCV", "ABCD", "EBCD", "EBAD" })]
    [DataRow("TOON", "PLEA", new[] { "POON", "PLEE", "SAME", "POIE", "PLEA", "PLIE", "POIN" }, new[] { "TOON", "POON", "POIN", "POIE", "PLIE", "PLEE", "PLEA" })]
    public void Parse_ReturnsAsExpected_ForValidInputs(string start, string end, string[] dictionary, string[] expected)
    {
      var mockLoader = new Mock<IDictionaryLoader>();
      mockLoader.Setup(obj => obj.Load()).Returns(dictionary);

      BreadthFirstDictionaryParser parser = new BreadthFirstDictionaryParser(start, end, mockLoader.Object);
      ParseResult result = parser.Parse();

      string[][] shortestPaths = result.GetShortestPaths();
      Assert.AreEqual(1, shortestPaths.Length);

      string[] actual = shortestPaths[0];
      Assert.AreEqual(expected.Length, actual.Length);

      for (int loop = 0; loop < expected.Length; loop++)
        Assert.AreEqual(expected[loop], actual[loop]);
    }
  }
}
