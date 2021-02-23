using System;

namespace WordLadder
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var txt = new Lib.TextFileDictionaryLoader(@"c:\temp\words-english.txt");
      string[] lines = txt.Load();

      //var zip = new Lib.ZipFileDictionaryLoader(@"c:\temp\words-english.zip");
      //lines = zip.Load();

      //InputArgs inputArgs = new InputArgs(args);
      //if (inputArgs.IsValid)
      //{
      //  Console.WriteLine("Hello World!");
      //  Console.ReadLine();
      //}
    }
  }
}
