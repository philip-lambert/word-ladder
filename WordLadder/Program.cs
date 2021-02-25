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
        IDictionaryLoader loader = DictionaryLoaderFactory.Create(inputArgs.Dictionary);
        IDictionaryParser parser = new BFSDictionaryParser(inputArgs.Start, inputArgs.End, loader);
        string[][] shortestPaths = parser.Parse();

        WriteOutput(inputArgs.Output, shortestPaths);
        ShowDone();
      }
      catch (Exception ex)
      {
        ShowError(ex);
      }
    }

    /// <summary>
    /// Returns true if args contains /? or /help
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
    /// Outputs the command line args to screen, with any errors shown in red
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

    private static void WriteOutput(string fileName, string[][] shortestPaths)
    {
      if (string.IsNullOrWhiteSpace(fileName))
        throw new ArgumentNullException(nameof(fileName));
      if (shortestPaths == null)
        throw new ArgumentNullException(nameof(shortestPaths));

      string[] text = shortestPaths.Select(obj => string.Join("->", obj)).ToArray();
      File.WriteAllLines(fileName, text);
    }

    private static void ShowDone()
    {
      WriteLine();
      WriteLine("Output file saved");
    }

    private static void ShowError(Exception ex)
    {
      if (ex == null)
        throw new ArgumentNullException(nameof(ex));

      WriteLine();
      WriteLine("An unknown error occurred: -");
      WriteLine(ex.Message, true);
    }

    /// <summary>
    /// Console.WriteLine wrapper
    /// </summary>
    /// <param name="value"></param>
    /// <param name="showAsError">If true, writes value in red</param>
    private static void WriteLine(string value = "", bool showAsError = false)
    {
      if (showAsError)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(value);
        Console.ResetColor();
      }
      else
        Console.WriteLine(value);
    }
  }
}
