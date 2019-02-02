using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Utils;

namespace ProjectEuler.Problems
{
  // Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).
  // If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.
  //
  // For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.
  //
  // Evaluate the sum of all the amicable numbers under 10000.

  class Problem21 : ProblemBase
  {
    private const int MAX_NUMBER = 10000;
    public ulong Solve()
    {
      uint[] m = new uint[MAX_NUMBER];
      m[1] = 1;
      for (uint i = 2; i < MAX_NUMBER; ++i)
      {
        m[i] = Dividers.GetSumOfDividers(i);
      }
      ulong sum = 0;
      for (uint a = 1; a < MAX_NUMBER; ++a)
      {
        uint b = m[a];
        if (b != 0 && b < MAX_NUMBER && a != b && m[b] == a)
        {
          sum += a;
        }
      }
      return sum;
    }
  }
}
