using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Utils
{
  class Word
  {
    /*
     * Sum of alphabetically indices of all letters.
     * 
     * Note: word must has uppercase latin characters
     * Example: SKY is 19 + 11 + 25 = 55
     */
    public static uint GetValue(string word)
    {
      Debug.Assert(word.All((ch) => ch >= 'A' && ch <= 'Z'));
      uint sum = 0;
      for (int j = 0; j < word.Length; ++j)
      {
        sum += (byte)word[j];
      }
      sum -= (uint)word.Length * 64;
      return sum;
    }
  }
}
