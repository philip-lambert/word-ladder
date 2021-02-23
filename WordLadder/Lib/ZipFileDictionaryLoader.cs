using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace WordLadder.Lib
{
  public class ZipFileDictionaryLoader : BaseDictionaryLoader
  {
    private readonly string _fileName;

    public ZipFileDictionaryLoader(string fileName)
    {
      if (string.IsNullOrWhiteSpace(fileName))
        throw new ArgumentNullException(nameof(fileName));

      _fileName = fileName;
    }

    protected override string[] PerformLoad()
    {
      List<string> result = new List<string>();

      using (ZipArchive archive = ZipFile.OpenRead(_fileName))
      {
        if (archive.Entries.Count != 1)
          throw new FileLoadException("Invalid zip file", _fileName);

        string fileName = Path.ChangeExtension(Path.GetFileName(_fileName), "txt").ToUpper();
        ZipArchiveEntry entry = archive.Entries[0];
        if (entry.FullName.ToUpper() != fileName)
          throw new FileLoadException("Invalid zip file", _fileName);

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
