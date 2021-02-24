using System;
using System.IO;

namespace WordLadder.Lib.DictionaryLoader
{
  /// <summary>
  /// Loads the contents of a text file into memory
  /// </summary>
  public class TextFileDictionaryLoader : IDictionaryLoader
  {
    private readonly string _dictionaryFile;

    public TextFileDictionaryLoader(string dictionaryFile)
    {
      if (string.IsNullOrWhiteSpace(dictionaryFile))
        throw new ArgumentNullException(nameof(dictionaryFile));

      _dictionaryFile = dictionaryFile;
    }

    public string[] Load()
    {
      return File.ReadAllLines(_dictionaryFile);
    }
  }
}
