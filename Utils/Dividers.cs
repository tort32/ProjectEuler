using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ProjectEuler.Utils
{
  static class Dividers
  {
    /*
     * Calculate the sum of deviders of x including 1, but excluding x
     * Exaplme for 12: 1 + 2 + 3 + 4 + 6 = 16
     */
    static public uint GetSumOfDividers(uint x)
    {
      Debug.Assert(x > 1, "The argument should be not less than 2");

      uint sqrtX = (uint) Math.Ceiling(Math.Sqrt(x));
      uint dividersSum = (x == sqrtX * sqrtX) ? sqrtX : 0;
      dividersSum += 1; // 1 is a divider for all
      for (uint i = 2u; i < sqrtX; ++i)
      {
        if (x % i == 0u)
        {
          dividersSum += i;
          dividersSum += x / i;
        }
      }
      return dividersSum;
    }

    /* 
     * Calculate the number of all deviders of x including 1 and x
     * Example 6 for 28: 1, 2, 4, 7, 14, 28
     */
    static public uint GetNumberOfDeviders(ulong x)
    {
      Debug.Assert(x > 1, "The argument should be not less than 2");
      
      uint sqrtX = (uint) Math.Ceiling(Math.Sqrt(x));
      uint dividersCount = (x == sqrtX * sqrtX) ? 3u : 2u;
      for (uint i = 2u; i < sqrtX; ++i)
      {
        if (x % i == 0u) dividersCount += 2;
      }

      return dividersCount;
    }

    /*
     * Calculate greatest common divisor of two positive integer numbers.
     * Example gcd(48, 180) = 12:
     *    48 = 2 × 2 × 2 × 2 × 3,
     *   180 = 2 × 2 ×         3 × 3 × 5,
     *    12 = 2 x 2 x         3.
     */
    static public ulong GreatestCommonDivisor(ulong a, ulong b)
    {
      ulong gcd = 1;
      PrimeNumbers primes = new PrimeNumbers();
      foreach (ulong p in primes)
      {
        if (a < p || b < p)
          break;
        while (a % p == 0 && b % p == 0)
        {
          gcd *= p;
          a /= p;
          b /= p;
        }
      }
      return gcd;
    }
  }
}
