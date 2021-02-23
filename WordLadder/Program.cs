using System;

namespace WordLadder
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var txt = DictionaryLoaderFactory.Create(@"c:\temp\words-english.txt");
      string[] lines = txt.Load();

      var zip = DictionaryLoaderFactory.Create(@"c:\temp\words-english.zip");
      lines = zip.Load();

      //InputArgs inputArgs = new InputArgs(args);
      //if (inputArgs.IsValid)
      //{
      //  Console.WriteLine("Hello World!");
      //  Console.ReadLine();
      //}
    }
  }
}
