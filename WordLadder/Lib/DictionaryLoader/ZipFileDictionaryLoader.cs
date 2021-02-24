using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace WordLadder.Lib.DictionaryLoader
{
  /// <summary>
  /// Loads the contents of a zipped text file into memory
  /// </summary>
  public class ZipFileDictionaryLoader : IDictionaryLoader
  {
    private readonly string _dictionaryFile;

    public ZipFileDictionaryLoader(string dictionaryFile)
    {
      if (string.IsNullOrWhiteSpace(dictionaryFile))
        throw new ArgumentNullException(nameof(dictionaryFile));

      _dictionaryFile = dictionaryFile;
    }

    public string[] Load()
    {
      List<string> result = new List<string>();

      using (ZipArchive archive = ZipFile.OpenRead(_dictionaryFile))
      {
        if (archive.Entries.Count != 1)
          throw new FileLoadException("Invalid zip file", _dictionaryFile);

        string fileName = Path.ChangeExtension(Path.GetFileName(_dictionaryFile), "txt").ToUpper();
        ZipArchiveEntry entry = archive.Entries[0];
        if (entry.FullName.ToUpper() != fileName)
          throw new FileLoadException("Invalid zip file", _dictionaryFile);

        using (Stream stream = entry.Open())
        {
          using (StreamReader streamReader = new StreamReader(stream))
          {
            string line = string.Empty;
            while ((line = streamReader.ReadLine()) != null)
            {
              result.Add(line);
            }
          }
        }
      }

      return result.ToArray();
    }
  }
}
