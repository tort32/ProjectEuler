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
      ulong number = mPrimes.Last();

      var watch = new System.Diagnostics.Stopwatch();
      watch.Start();

      // Computing Eratosthenes sieve
      const int SIEVE_SIZE = 2097152;
      if (number < SIEVE_SIZE)
      {
        // Bit array present only odd-value numbers
        // So starting from prime 3
        BitArray sieve = new BitArray(SIEVE_SIZE/2, true);
        for (int nPrime = 1; nPrime < mPrimes.Count; ++nPrime)
        {
          int prime = (int) mPrimes[nPrime];

          if (prime <= (SIEVE_SIZE - 1)/prime)
          {
            // Markout multiples of prime number starting (p^2) stepping 2p
            for (int idx = prime*prime; idx < SIEVE_SIZE; idx += 2*prime)
            {
              sieve[(idx-1)/2] = false;
            }
          }

          // Add next prime from the sieve
          if (nPrime == mPrimes.Count - 1)
          {
            for (int idx = (prime + 2); idx < SIEVE_SIZE; idx += 2)
            {
              if (sieve[(idx - 1)/2])
              {
                ulong next_prime = (ulong) idx;
                mPrimes.Add(next_prime);
                yield return next_prime;
                break;
              }
            }
          }
        }
        number = mPrimes.Last();
      }

      watch.Stop();
      Console.WriteLine("Computing Eratosfene sieve for {0}: {1}", SIEVE_SIZE, watch.Elapsed);

      while (true)
      {
        number += (number%6 == 1) ? 4U : 2U;

        ulong firstDevider =
          mPrimes.TakeWhile(prime => prime <= Math.Sqrt(number)).FirstOrDefault(prime => number%prime == 0);

        if (firstDevider == 0)
        {
          mPrimes.Add(number);
          yield return number;
        }

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

    private static List<ulong> mPrimes;
  }
}
