using System;

using WordLadder.Lib.DictionaryLoader;

namespace WordLadder
{
  public class Program
  {
    public static void Main(string[] args)
    {
      if (args != null && args.Length == 1 && (args[0] == "/?" || args[0].Equals("/help", StringComparison.OrdinalIgnoreCase)))
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

      IDictionaryLoader dictionaryLoader = DictionaryLoaderFactory.Create(inputArgs.Dictionary);

      Console.WriteLine("Get on with it!");
      Console.ReadLine();
    }

    private static void ShowHelp(InputArgs inputArgs)
    {
      WriteLine("Computes a list of words which move from the start word to the end word in the shortest number of steps.", false);
      WriteLine(string.Empty, false);
      WriteLine($"  {InputArgs.START_ARG}\t\tFour letter start word i.e. 'same'", inputArgs != null && !inputArgs.StartIsValid);
      WriteLine($"  {InputArgs.END_ARG}\t\t\tFour letter end word i.e. 'cost'", inputArgs != null && !inputArgs.EndIsValid);
      WriteLine($"  {InputArgs.DICTIONARY_ARG}\t\tDictionary file name", inputArgs != null && !inputArgs.DictionaryIsValid);
      WriteLine($"  {InputArgs.OUTPUT_ARG}\t\tAnswer file name", inputArgs != null && !inputArgs.OutputIsValid);
    }

    private static void WriteLine(string str, bool showAsError)
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
