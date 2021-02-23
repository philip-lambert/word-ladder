using System;
using System.IO;

namespace WordLadder.Lib
{
  public class TextFileDictionaryLoader : BaseDictionaryLoader
  {
    private readonly string _fileName;

    public TextFileDictionaryLoader(string fileName)
    {
      if (string.IsNullOrWhiteSpace(fileName))
        throw new ArgumentNullException(nameof(fileName));

      _fileName = fileName;
    }

    protected override string[] PerformLoad()
    {
      return File.ReadAllLines(_fileName);
    }
  }
}
