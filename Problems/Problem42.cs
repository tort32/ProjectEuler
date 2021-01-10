using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ProjectEuler.Utils;

namespace ProjectEuler.Problems
{
  class Problem42 : ProblemBase
  {
    private const int WORD_LENGHT_MAX = 14; // Queried from words list
    private const int LETTER_VALUE_MAX = 26;
    private const int WORD_VALUE_MAX = WORD_LENGHT_MAX * LETTER_VALUE_MAX;
    public ulong Solve()
    {
      uint[] tnums = GetTriangleNumbers().TakeWhile((n) => n <= WORD_VALUE_MAX).ToArray();
      SortedSet<uint> triangleNimbers = new SortedSet<uint>(tnums);

      string[] words = GetWords().ToArray();
      int count = words.Count((w) => triangleNimbers.Contains(Word.GetValue(w)));
      
      return (ulong) count;
    }

    protected uint WordValue(string word)
    {
      uint sum = 0;
      for (int j = 0; j < word.Length; ++j)
      {
        sum += (byte)word[j];
      }
      sum -= (uint)word.Length * 64;
      return sum;
    }

    private IEnumerable<uint> GetTriangleNumbers()
    {
      uint n = 0;
      uint sum = 0;
      while(true)
      {
        sum += (++n);
        yield return sum;
      }
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
}
