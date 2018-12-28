﻿using System;
using ProjectEuler.Utils;

namespace ProjectEuler.Problems
{
  class Problem27 : ProblemBase
  {
    public ulong Solve()
    {
      PrimeNumbers primes = new PrimeNumbers();

      long ab = Int64.MaxValue;
      int nMax = 0;
      for (int a = 9; a >= -9; --a)
      {
        int bIdx = 0;
        int b = (int)primes[bIdx];
        while(b < 100)
        {
          int n = 0;
          do
          {
            int m = n * n + a * n + b;
            if (m <= 0)
              break;
            ulong m2 = (ulong) m;
            if (!primes.isPrime(m2))
              break;
            ++n;
          } while (true);
          if(n > nMax)
          {
            nMax = n;
            ab = a * b;
          }
          ++bIdx;
        }
      }
      Console.Out.WriteLine(ab);
      return (ulong)ab;
    }
  }
}
