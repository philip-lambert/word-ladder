﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace WordLadder
{
  /// <summary>
  /// Parses the command line args
  /// </summary>
  public class InputArgs
  {
    public const string START_ARG = "/start";
    public const string END_ARG = "/end";
    public const string DICTIONARY_ARG = "/dictionary";
    public const string OUTPUT_ARG = "/output";

    public string Start { get; private set; } = null;
    public bool StartIsValid => !string.IsNullOrWhiteSpace(Start);
    public string End { get; private set; } = null;
    public bool EndIsValid => !string.IsNullOrWhiteSpace(End);
    public string Dictionary { get; private set; } = null;
    public bool DictionaryIsValid => !string.IsNullOrWhiteSpace(Dictionary);
    public string Output { get; private set; } = null;
    public bool OutputIsValid => !string.IsNullOrWhiteSpace(Output);
    public bool IsValid => StartIsValid && EndIsValid && DictionaryIsValid && OutputIsValid;

    public InputArgs(string[] args)
    {
      if (args == null || args.Length == 0)
        return;

      if (args.Length == 4 && !args.Any(obj => obj.StartsWith('/')))
      {
        Start = args[0];
        End = args[1];
        Dictionary = args[2];
        Output = args[3];
        return;
      }

      List<string> _args = args.ToList();
      Start = GetNamedArg(_args, START_ARG);
      End = GetNamedArg(_args, END_ARG);
      Dictionary = GetNamedArg(_args, DICTIONARY_ARG);
      Output = GetNamedArg(_args, OUTPUT_ARG);
    }

    private string GetNamedArg(List<string> args, string argName)
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
