using System;
using System.Collections.Generic;
using ProjectEuler.Utils;

namespace ProjectEuler.Problems
{
  class Problem30 : ProblemBase
  {
    public ulong Solve()
    {
      ulong s = 0;
      const int digitsCount = 6;
      byte[] digits = new byte[digitsCount];
      digits[0] = 2;
      ulong n = 2;
      ulong[] pows = new ulong[10];
      for (ulong i = 0; i < 10; ++i)
      {
        pows[i] = i * i * i * i * i;
      }

      do
      {
        ++n;
        ++digits[0];
        for (int i = 0; i < digitsCount - 1; ++i)
        {
          if (digits[i] == 10)
          {
            digits[i] = 0;
            ++digits[i + 1];
          }
        }
        if (digits[digitsCount - 1] == 10)
          break;
        ulong m = 0;
        for (int i = 0; i < digitsCount; ++i)
        {
          m += pows[digits[i]];
        }
        if (n == m)
        {
          s += n;
          String nStr = "";
          for (int i = digitsCount - 1; i >= 0; --i)
            nStr += digits[i];
          Console.Out.WriteLine(nStr);
        }
      } while (true);

      return s;
    }
  }
}
