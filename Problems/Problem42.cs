using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ProjectEuler;
using ProjectEuler.Utils;

internal class Problem42 : ProblemBase
{
  private const int WORD_LENGHT_MAX = 14; // Queried from words list
  private const int LETTER_VALUE_MAX = 26;
  private const int WORD_VALUE_MAX = WORD_LENGHT_MAX * LETTER_VALUE_MAX;
  public ulong Solve()
  {
    ulong[] tnums = PolygonalNumbers.Triangular().TakeWhile((n) => n <= WORD_VALUE_MAX).ToArray();
    var tset = new SortedSet<ulong>(tnums);

    string[] words = GetWords().ToArray();
    int count = words.Count((w) => tset.Contains(Word.GetValue(w)));
      
    return (ulong) count;
  }

  private IEnumerable<string> GetWords()
  {
    Assembly assembly = Assembly.GetExecutingAssembly();
    using (Stream stream = assembly.GetManifestResourceStream("ProjectEuler.Resources.p042_words.txt"))
    {
      using (StreamReader reader = new StreamReader(stream))
      {
        string result = reader.ReadToEnd();
        foreach (string line in result.Split('\n'))
        {
          if (line.Equals(String.Empty))
            continue;
          yield return line;
        }
      }
    }
  }
}
