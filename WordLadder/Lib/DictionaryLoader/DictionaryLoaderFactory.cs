using System;
using System.IO;

namespace WordLadder.Lib.DictionaryLoader
{
  /// <summary>
  /// Factory class to create a dictionary loader. Logic based on the dictionaryFile extension (txt or zip)
  /// </summary>
  public class DictionaryLoaderFactory
  {
    private string _dictionaryFile;

    public DictionaryLoaderFactory(string dictionaryFile)
    {
      if (string.IsNullOrWhiteSpace(dictionaryFile))
        throw new ArgumentNullException(nameof(dictionaryFile));

      _dictionaryFile = dictionaryFile;
    }

    /// <summary>
    /// Returns a dictionary loader
    /// </summary>
    /// <exception cref="NotSupportedException">Thrown if extension not supported</exception>
    /// <returns></returns>
    public IDictionaryLoader Create()
    {
      string extension = Path.GetExtension(_dictionaryFile).ToLower();
      if (extension == ".txt")
        return new TextFileDictionaryLoader(_dictionaryFile);
      if (extension == ".zip")
        return new ZipFileDictionaryLoader(_dictionaryFile);

      throw new NotSupportedException($"File extension '{extension}' not supported.");
    }

    public static IDictionaryLoader Create(string dictionaryFile)
    {
      IDictionaryLoader result = new DictionaryLoaderFactory(dictionaryFile).Create();
      return result;
    }
  }
}
