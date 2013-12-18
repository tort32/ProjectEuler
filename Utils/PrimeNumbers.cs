using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Utils
{
  class PrimeNumbers : IEnumerable<int>
  {
    static PrimeNumbers()
    {
      mPrimes = new List<int> { 2, 3 };
    }

    public IEnumerator<int> GetEnumerator()
    {
      foreach (int prime in mPrimes)
      {
        yield return prime;
      }
      while (true)
      {
        yield return ComputeNextPrime(mPrimes.Last() + 2);
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public int this[int index]
    {
      get
      {
        if (index < mPrimes.Count)
        {
          return mPrimes[index];
        }
        else
        {
          return this.Skip(index).First();
        }
      }
    }

    private int ComputeNextPrime(int starting)
    {
      int number = starting;
      while (true)
      {
        int firstDevider = mPrimes.TakeWhile(prime => prime <= Math.Sqrt(number)).FirstOrDefault(prime => number % prime == 0);
        if (firstDevider == 0)
        {
          mPrimes.Add(number);
          return number;
        }
        number += 2;
      }
    }

    private static List<int> mPrimes;
  }
}
