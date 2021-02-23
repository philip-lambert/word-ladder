using System.Linq;

namespace WordLadder.Lib
{
  public abstract class BaseDictionaryLoader
  {
    protected abstract string[] PerformLoad();

    public string[] Load()
    {
      string[] result = PerformLoad();
      result = result
        .Where(obj => obj.IsValidWord())
        .ToArray();
      return result;
    }
  }
}
