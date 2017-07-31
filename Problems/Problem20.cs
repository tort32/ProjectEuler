using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Utils;

namespace ProjectEuler.Problems
{
  class Problem20 : ProblemBase
  {
    private const uint DIGITS_COUNT = 160;

    public ulong Solve()
    {
      BigNumber n = new BigNumber(DIGITS_COUNT, 1);
      for (int i = 2; i <= 100; ++i)
      {
        BigNumber m = new BigNumber(n);
        for (int j = 1; j < i; ++j)
        {
          n.Add(m);
        }
      }
      uint sum = 0;
      for (int i = 0; i < DIGITS_COUNT; ++i)
      {
        sum += n.GetDigitAt(i);
      }
      return sum;
    }
  }
}
