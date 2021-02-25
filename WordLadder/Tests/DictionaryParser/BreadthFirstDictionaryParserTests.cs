using System.Linq;

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
    public void Parse_ReturnsSingleResult_ForValidInputs(string start, string end, string[] dictionary, string[] expected)
    {
      var mockLoader = new Mock<IDictionaryLoader>();
      mockLoader.Setup(obj => obj.Load()).Returns(dictionary);

      IDictionaryParser parser = new BFSDictionaryParser(start, end, mockLoader.Object);
      string[][] shortestPaths = parser.Parse();
      Assert.AreEqual(1, shortestPaths.Length);

      string[] actual = shortestPaths[0];
      Assert.AreEqual(expected.Length, actual.Length);

      for (int loop = 0; loop < expected.Length; loop++)
        Assert.AreEqual(expected[loop], actual[loop]);
    }

    [TestMethod]
    public void Parse_ReturnsMultipleResult_ForValidInputs()
    {
      string start = "nape";
      string end = "mild";
      string[] dictionary = new[] { "dose", "ends", "dine", "jars", "prow", "soap", "guns", "hops", "cray", "hove", "ella",
        "hour", "lens", "jive", "wiry", "earl", "mara", "part", "flue", "putt", "rory", "bull", "york", "ruts", "lily", "vamp",
        "bask", "peer", "boat", "dens", "lyre", "jets", "wide", "rile", "boos", "down", "path", "onyx", "mows", "toke", "soto",
        "dork", "nape", "mans", "loin", "jots", "male", "sits", "minn", "sale", "pets", "hugo", "woke", "suds", "rugs", "vole",
        "warp", "mite", "pews", "lips", "pals", "nigh", "sulk", "vice", "clod", "iowa", "gibe", "shad", "carl", "huns", "coot",
        "sera", "mils", "rose", "orly", "ford", "void", "time", "eloy", "risk", "veep", "reps", "dolt", "hens", "tray", "melt",
        "rung", "rich", "saga", "lust", "yews", "rode", "many", "cods", "last", "tile", "nosy", "take", "nope", "toni", "bank",
        "jock", "jody", "diss", "nips", "bake", "lima", "wore", "kins", "cult", "hart", "wuss", "tale", "sing", "lake", "bogy",
        "wigs", "kari", "magi", "bass", "pent", "tost", "fops", "bags", "duns", "will", "tart", "drug", "gale", "mold", "disk",
        "spay", "hows", "naps", "puss", "gina", "kara", "zorn", "boll", "cams", "boas", "rave", "sets", "lego", "hays", "judy",
        "chap", "live", "bahs", "ohio", "nibs", "cuts", "pups", "data", "kate", "rump", "hews", "mary", "stow", "fang", "bolt",
        "rues", "mesh", "mice", "rise", "rant", "dune", "jell", "laws", "jove", "bode", "sung", "nils", "vila", "mode", "hued",
        "cell", "fies", "swat", "wags", "nate", "wist", "honk", "goth", "told", "oise", "wail", "tels", "sore", "hunk", "mate",
        "luke", "tore", "bond", "bast", "vows", "ripe", "fond", "benz", "firs", "zeds", "wary", "baas", "wins", "pair", "tags",
        "cost", "woes", "buns", "lend", "bops", "code", "eddy", "siva", "oops", "toed", "bale", "hutu", "jolt", "rife", "darn",
        "tape", "bold", "cope", "cake", "wisp", "vats", "wave", "hems", "bill", "cord", "pert", "type", "kroc", "ucla", "albs",
        "yoko", "silt", "pock", "drub", "puny", "fads", "mull", "pray", "mole", "talc", "east", "slay", "jamb", "mill", "dung",
        "jack", "lynx", "nome", "leos", "lade", "sana", "tike", "cali", "toge", "pled", "mile", "mass", "leon", "sloe", "lube",
        "kans", "cory", "burs", "race", "toss", "mild", "tops", "maze", "city", "sadr", "bays", "poet", "volt", "laze", "gold",
        "zuni", "shea", "gags", "fist", "ping", "pope", "cora", "yaks", "cosy", "foci", "plan", "colo", "hume", "yowl", "craw",
        "pied", "toga", "lobs", "love", "lode", "duds", "bled", "juts", "gabs", "fink", "rock", "pant", "wipe", "pele", "suez",
        "nina", "ring", "okra", "warm", "lyle", "gape", "bead", "lead", "jane", "oink", "ware", "zibo", "inns", "mope", "hang",
        "made", "fobs", "gamy", "fort", "peak", "gill", "dino", "dina", "tier" };

      var mockLoader = new Mock<IDictionaryLoader>();
      mockLoader.Setup(obj => obj.Load()).Returns(dictionary);

      IDictionaryParser parser = new BFSDictionaryParser(start, end, mockLoader.Object);
      string[][] shortestPaths = parser.Parse();
      Assert.AreEqual(3, shortestPaths.Length);
      Assert.IsTrue(shortestPaths.All(obj => obj.Length == 6));
    }
  }
}
