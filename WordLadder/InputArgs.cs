using System;
using System.Linq;
using System.Collections.Generic;

namespace WordLadder
{
  public class InputArgs
  {
    #region Constants/Fields/Properties

    public const string START_ARG = "/start";
    public const string END_ARG = "/end";
    public const string DICTIONARY_ARG = "/dictionary";
    public const string OUTPUT_ARG = "/output";

    private string _start = null;
    private string _end = null;
    private string _dictionary = null;
    private string _output = null;

    public string Start => _start;
    public bool StartIsValid => !string.IsNullOrWhiteSpace(_start);
    public string End => _end;
    public bool EndIsValid => !string.IsNullOrWhiteSpace(_end);
    public string Dictionary => _dictionary;
    public bool DictionaryIsValid => !string.IsNullOrWhiteSpace(_dictionary);
    public string Output => _output;
    public bool OutputIsValid => !string.IsNullOrWhiteSpace(_output);
    public bool IsValid => StartIsValid && EndIsValid && DictionaryIsValid && OutputIsValid;

    #endregion

    #region ctor

    public InputArgs(string[] args)
    {
      if (args == null || args.Length == 0)
        return;

      List<string> _args = args.ToList();

      if (_args.Count == 4)
      {
        _start = _args[0];
        _end = _args[1];
        _dictionary = _args[2];
        _output = _args[3];
        return;
      }

      if (_args.Count >= 8)
      {
        _start = GetArg(_args, START_ARG);
        _end = GetArg(_args, END_ARG);
        _dictionary = GetArg(_args, DICTIONARY_ARG);
        _output = GetArg(_args, OUTPUT_ARG);
      }
    }

    #endregion

    private string GetArg(List<string> args, string argName)
    {
      if (args == null)
        throw new ArgumentNullException(nameof(args));
      if (string.IsNullOrWhiteSpace(argName))
        throw new ArgumentNullException(nameof(argName));

      int idx = args.FindIndex(obj => obj.Equals(argName, StringComparison.OrdinalIgnoreCase));
      if (idx != -1 && idx < args.Count - 1)
        return args[idx + 1];
      return null;
    }
  }
}
