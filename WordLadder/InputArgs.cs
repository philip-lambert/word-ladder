using System;
using System.IO;
using System.Linq;

namespace WordLadder
{
  public class InputArgs
  {
    #region Fields/Properties

    private string _start = null;
    private string _end = null;
    private string _dictionary = null;
    private string _output = null;
    private bool? _caseSensitive = null;

    private bool StartIsValid => _start != null;
    private bool EndIsValid => _end != null;
    private bool DictionaryIsValid => _dictionary != null;
    private bool OutputIsValid => _output != null;
    private bool CaseSensitiveIsValid => _caseSensitive != null;

    public string Start => _start;
    public string End => _end;
    public string Dictionary => _dictionary;
    public string Output => _output;
    public bool CaseSensitive => _caseSensitive.HasValue && _caseSensitive.Value;
    public bool IsValid => StartIsValid && EndIsValid && DictionaryIsValid && OutputIsValid && CaseSensitiveIsValid;

    #endregion

    #region ctor

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
        _start = str;

      str = GetArg(args, "/end:");
      if (str != null)
        _end = str;

      str = GetArg(args, "/dictionary:");
      if (FileIsValid(str, FileMode.Open))
        _dictionary = str;

      str = GetArg(args, "/output:");
      if (FileIsValid(str, FileMode.Create))
        _output = str;

      str = GetArg(args, "/case-sensitive:");
      if (str != null)
      {
        str = str.ToUpper();
        if (str == "TRUE" || str == "FALSE")
          _caseSensitive = str == "TRUE";
      }
      else
        _caseSensitive = false;

      if (!IsValid)
        ShowHelp(true);
    }

    #endregion

    public void ShowHelp(bool showErrors)
    {
      Console.WriteLine("Compute a list of words which move from the start word to the end word in the shortest number of steps.");
      Console.WriteLine();
      WriteLine("  /start:\t\tFour letter start word i.e. 'same'", showErrors && !StartIsValid);
      WriteLine("  /end:\t\t\tFour letter end word i.e. 'cost'", showErrors && !EndIsValid);
      WriteLine("  /dictionary:\t\tDictionary file name", showErrors && !DictionaryIsValid);
      WriteLine("  /output:\t\tAnswer file name", showErrors && !OutputIsValid);
      WriteLine("  /case-sensitive:\tSearch is case sensitive. Valid values are 'true' or 'false' (defaults to 'false')", showErrors && !CaseSensitiveIsValid);
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

    private void WriteLine(string str, bool showAsError)
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
