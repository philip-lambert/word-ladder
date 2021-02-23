using System;

namespace WordLadder
{
  public class Program
  {
    public static void Main(string[] args)
    {
      InputArgs inputArgs = new InputArgs(args);
      if (inputArgs.IsValid)
      {
        Console.WriteLine("Hello World!");
        Console.ReadLine();
      }
    }
  }
}
