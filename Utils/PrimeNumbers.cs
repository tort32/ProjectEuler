using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Utils
{
  class PrimeNumbers : IEnumerable<ulong>
  {
    static PrimeNumbers()
    {
      mPrimes = new List<ulong> { 2, 3 };
    }

    public IEnumerator<ulong> GetEnumerator()
    {
      foreach (ulong prime in mPrimes)
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

    public ulong this[int index]
    {
      get { return (index < mPrimes.Count) ? mPrimes[index] : this.Skip(index).First(); }
    }

    private ulong ComputeNextPrime(ulong starting)
    {
      ulong number = starting;
      while (true)
      {
        ulong firstDevider = mPrimes.TakeWhile(prime => prime <= Math.Sqrt(number)).FirstOrDefault(prime => number % prime == 0);
        if (firstDevider == 0)
        {
          mPrimes.Add(number);
          return number;
        }
        number += (number % 6 == 1) ? 4U : 2U;
        //number += 2;
      }
    }

    private static List<ulong> mPrimes;
  }
}
