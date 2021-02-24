using System;
using System.IO;
using System.Linq;

using WordLadder.Lib.DictionaryLoader;
using WordLadder.Lib.DictionaryParser;

namespace WordLadder
{
  public class Program
  {
    public static void Main(string[] args)
    {
      if (HelpRequested(args))
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
        DictionaryLoaderFactory loaderFactory = new DictionaryLoaderFactory(inputArgs.Dictionary);
        IDictionaryLoader loader = loaderFactory.Create();
        IDictionaryParser parser = new DictionaryParser(inputArgs.Start, inputArgs.End, loader);
        ParseResult result = parser.Parse();
        OutputResult(inputArgs.Output, result.GetShortestPaths());
      }
      catch (Exception ex)
      {
        WriteLine();
        WriteLine("An unknown error occurred: -");
        WriteLine(ex.Message, true);
      }
    }

    /// <summary>
    /// Returns true if the application was run with /? or /help
    /// </summary>
    /// <param name="args">The command line args</param>
    /// <returns></returns>
    private static bool HelpRequested(string[] args)
    {
      bool result = args != null &&
        args.Length == 1 &&
        (args[0] == "/?" || args[0].Equals("/help", StringComparison.OrdinalIgnoreCase));
      return result;
    }

    /// <summary>
    /// Outputs the command line args, with any errors shown in red
    /// </summary>
    /// <param name="inputArgs"></param>
    private static void ShowHelp(InputArgs inputArgs)
    {
      WriteLine("Computes a list of words which move from the start word to the end word in the shortest number of steps.");
      WriteLine();
      WriteLine($"  {InputArgs.START_ARG}\t\tFour letter start word i.e. 'same'", inputArgs != null && !inputArgs.StartIsValid);
      WriteLine($"  {InputArgs.END_ARG}\t\t\tFour letter end word i.e. 'cost'", inputArgs != null && !inputArgs.EndIsValid);
      WriteLine($"  {InputArgs.DICTIONARY_ARG}\t\tDictionary text file/zip file", inputArgs != null && !inputArgs.DictionaryIsValid);
      WriteLine($"  {InputArgs.OUTPUT_ARG}\t\tAnswer file", inputArgs != null && !inputArgs.OutputIsValid);
    }

    /// <summary>
    /// Outputs the shorted path(s) to the supplied file name
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="shortestPaths"></param>
    private static void OutputResult(string fileName, string[][] shortestPaths)
    {
      if (string.IsNullOrWhiteSpace(fileName))
        throw new ArgumentNullException(nameof(fileName));
      if (shortestPaths == null)
        throw new ArgumentNullException(nameof(shortestPaths));

      string[] text = shortestPaths.Select(obj => string.Join("->", obj)).ToArray();
      File.WriteAllLines(fileName, text);
    }

    /// <summary>
    /// Writes supplied string to the screen, in red if showAsError is true
    /// </summary>
    /// <param name="str"></param>
    /// <param name="showAsError"></param>
    private static void WriteLine(string str = "", bool showAsError = false)
    {
      if (showAsError)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(str);
        Console.ResetColor();
      }
      else
        Console.WriteLine(str);
    }
  }
}
