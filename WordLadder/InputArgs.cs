using System;
using System.IO;
using System.Linq;

namespace WordLadder
{
  public class InputArgs
  {
    public string Start { get; private set; } = null;
    public string End { get; private set; } = null;
    public string Dictionary { get; private set; } = null;
    public string Output { get; private set; } = null;
    public bool? CaseSensitive { get; private set; } = null;
    public bool IsValid => Start != null && End != null && Dictionary != null && Output != null && CaseSensitive != null;

    public InputArgs(string[] args)
    {
      if (args == null || args.Length == 0)
        return;

      if (args.Length == 1 && args[0] == "/?")
      {
        ShowHelp(false);
        return;
      }

      string str = GetArg(args, "/start:");
      if (str != null)
        Start = str;

      str = GetArg(args, "/end:");
      if (str != null)
        End = str;

      str = GetArg(args, "/dictionary:");
      if (FileIsValid(str, FileMode.Open))
        Dictionary = str;

      str = GetArg(args, "/output:");
      if (FileIsValid(str, FileMode.Create))
        Output = str;

      str = GetArg(args, "/case-sensitive:");
      if (str != null)
      {
        str = str.ToUpper();
        if (str == "TRUE" || str == "FALSE")
          CaseSensitive = str == "TRUE";
      }

      if (!IsValid)
        ShowHelp(true);
    }

    public void ShowHelp(bool showErrors)
    {
      Console.WriteLine("Compute a list of words which move from the start word to the end word in the shortest number of steps.");
      Console.WriteLine();
      Console.WriteLine("  /start:\tFour letter start word i.e. 'same'");
      Console.WriteLine("  /end:\t\tFour letter end word i.e. 'cost'");
      Console.WriteLine("  /dictionary:\tDictionary file name");
      Console.WriteLine("  /output:\tAnswer file name");
    }

    private string GetArg(string[] args, string argName)
    {
      if (args == null || args.Length == 0)
        return null;
      if (string.IsNullOrWhiteSpace(argName))
        throw new ArgumentNullException(nameof(argName));

      string[] strs = args.Where(obj => obj.StartsWith(argName, StringComparison.InvariantCultureIgnoreCase)).ToArray();
      if (strs.Length != 1)
        return null;

      string result = strs[0].Substring(argName.Length).Trim();
      if (result.Length == 0)
        return null;

      return result;
    }

    /// <summary>
    /// Returns false if the file can't be opened using the supplied file mode
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileMode"></param>
    /// <returns></returns>
    private bool FileIsValid(string fileName, FileMode fileMode)
    {
      if (string.IsNullOrWhiteSpace(fileName))
        return false;

      FileStream fStr = null;
      try
      {
        fStr = new FileStream(fileName, fileMode);
      }
      catch (Exception ex) when (ex is FileNotFoundException || ex is UnauthorizedAccessException)
      {
        return false;
      }
      finally
      {
        if (fStr != null)
          fStr.Dispose();
      }
      return true;
    }
  }
}
