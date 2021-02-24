using System;
using System.IO;

namespace WordLadder.Lib.DictionaryLoader
{
  public class DictionaryLoaderFactory
  {
    private string _dictionaryFile;

    public DictionaryLoaderFactory(string dictionaryFile)
    {
      if (string.IsNullOrWhiteSpace(dictionaryFile))
        throw new ArgumentNullException(nameof(dictionaryFile));

      _dictionaryFile = dictionaryFile;
    }

    public IDictionaryLoader Create()
    {
      string extension = Path.GetExtension(_dictionaryFile).ToLower();
      if (extension == ".txt")
        return new TextFileDictionaryLoader(_dictionaryFile);
      if (extension == ".zip")
        return new ZipFileDictionaryLoader(_dictionaryFile);

      throw new NotSupportedException($"File extension '{extension}' not supported.");
    }
  }
}
