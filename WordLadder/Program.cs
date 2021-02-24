using System;
using System.IO;

using WordLadder.Lib.DictionaryLoader;
using WordLadder.Lib.DictionaryParser;

namespace WordLadder
{
  public class Program
  {
    public static void Main(string[] args)
    {
      bool helpRequested = args != null && args.Length == 1 && (args[0] == "/?" || args[0].Equals("/help", StringComparison.OrdinalIgnoreCase));
      if (helpRequested)
      {
        ShowHelp(null);
        return;
      }

      InputArgs inputArgs = new InputArgs(args);
      if (!inputArgs.IsValid)
      {
        ShowHelp(inputArgs);
        return;
      }

      try
      {
        IDictionaryLoader loader = DictionaryLoaderFactory.Create(inputArgs.Dictionary);
        DictionaryParser parser = new DictionaryParser(inputArgs.Start, inputArgs.End, loader);
        string[] result = parser.Parse();
        File.WriteAllLines(inputArgs.Output, result);
      }
      catch (Exception ex)
      {
        WriteLine();
        WriteLine("An unknown error occurred: -");
        WriteLine(ex.Message, true);
      }
    }

    private static void ShowHelp(InputArgs inputArgs)
    {
      WriteLine("Computes a list of words which move from the start word to the end word in the shortest number of steps.");
      WriteLine();
      WriteLine($"  {InputArgs.START_ARG}\t\tFour letter start word i.e. 'same'", inputArgs != null && !inputArgs.StartIsValid);
      WriteLine($"  {InputArgs.END_ARG}\t\t\tFour letter end word i.e. 'cost'", inputArgs != null && !inputArgs.EndIsValid);
      WriteLine($"  {InputArgs.DICTIONARY_ARG}\t\tDictionary file name", inputArgs != null && !inputArgs.DictionaryIsValid);
      WriteLine($"  {InputArgs.OUTPUT_ARG}\t\tAnswer file name", inputArgs != null && !inputArgs.OutputIsValid);
    }

    private static void WriteLine(string str = "", bool showAsError = false)
    {
      if (!showAsError)
        Console.WriteLine(str);
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(str);
        Console.ResetColor();
      }
    }
  }
}
