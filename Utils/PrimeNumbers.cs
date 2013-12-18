using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Utils
{
  class PrimeNumbers : IEnumerable<decimal>
  {
    static PrimeNumbers()
    {
      mPrimes = new List<decimal> { 2 };
    }
    public IEnumerator<decimal> GetEnumerator()
    {
      foreach (decimal prime in mPrimes)
      {
        //Console.WriteLine(prime);
        yield return prime;
      }
      decimal number = mPrimes.Last() + 1;
      while (true)
      {
        decimal firstDevider = mPrimes.FirstOrDefault(prime => number % prime == 0);
        if (firstDevider == 0)
        {
          //Console.WriteLine(number);
          mPrimes.Add(number);
          yield return number;
        }
        ++number;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public decimal this[int i]
    {
      get
      {
        if (i < mPrimes.Count)
        {
          return mPrimes[i];
        }
        else
        {
          var enumerable = this.Take(i + 1);
          decimal value = enumerable.Last();
          return value;
        }
      }
    }

    private static List<decimal> mPrimes;
  }
}
