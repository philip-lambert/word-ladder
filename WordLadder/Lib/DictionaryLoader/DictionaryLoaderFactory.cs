using System;
using System.IO;

namespace WordLadder.Lib.DictionaryLoader
{
  public static class DictionaryLoaderFactory
  {
    public static IDictionaryLoader Create(string dictionaryFile)
    {
      if (string.IsNullOrWhiteSpace(dictionaryFile))
        throw new ArgumentNullException(nameof(dictionaryFile));

      string extension = Path.GetExtension(dictionaryFile).ToLower();
      if (extension == ".txt")
        return new TextFileDictionaryLoader(dictionaryFile);
      if (extension == ".zip")
        return new ZipFileDictionaryLoader(dictionaryFile);

      throw new ArgumentException($"File extension '{extension}' not supported.");
    }
  }
}
