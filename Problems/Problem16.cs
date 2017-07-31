using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Utils;

namespace ProjectEuler.Problems
{
  class Problem16 : ProblemBase
  {
    private const uint DIGITS_COUNT = 302;

    public ulong Solve()
    {
      BigNumber n = new BigNumber(DIGITS_COUNT, 2);
      for (int i = 1; i < 1000; ++i)
      {
        BigNumber m = new BigNumber(n);
        n.Add(m); // n + n = 2 * n
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
