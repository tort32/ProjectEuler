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
      if (number < SIEVE_SIZE)
      {
        // Bit array present only odd-value numbers
        // So starting from prime 3
        BitArray sieve = new BitArray(SIEVE_SIZE / 2, true);
        for (int nPrime = 1; nPrime < mPrimes.Count; ++nPrime)
        {
          int prime = (int)mPrimes[nPrime];

          if (prime <= (SIEVE_SIZE - 1) / prime)
          {
            // Markout multiples of prime number starting (p^2) stepping 2p
            for (int idx = prime * prime; idx < SIEVE_SIZE; idx += 2 * prime)
            {
              sieve[(idx - 1) / 2] = false;
            }
          }

          // Add next prime from the sieve
          if (nPrime == mPrimes.Count - 1)
          {
            for (int idx = (prime + 2); idx < SIEVE_SIZE; idx += 2)
            {
              if (sieve[(idx - 1) / 2])
              {
                ulong next_prime = (ulong)idx;
                mPrimes.Add(next_prime);
                yield return next_prime;
                break;
              }
            }
          }
        }
        mSieve = sieve; // Eratosthenes sieve is filled
        number = mPrimes.Last();
      }

      watch.Stop();
      Console.WriteLine("Computing Eratosfene sieve for {0}: {1}", SIEVE_SIZE, watch.Elapsed);

      while (true)
      {
        number += (number % 6 == 1) ? 4U : 2U;

        ulong firstDevider =
          mPrimes.TakeWhile(prime => prime <= Math.Sqrt(number)).FirstOrDefault(prime => number % prime == 0);

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

    public bool IsPrime(ulong n)
    {
      if (n == 1)
        return true;
      if (n % 2 == 0)
        return false;
      // Search in primes number sieve
      if (mSieve != null && n < SIEVE_SIZE)
      {
        int sieveIdx = (int)((n - 1) / 2);
        if (mSieve[sieveIdx])
          return true;
      }
      // Search in primes number seqence
      int index = IndexLookupTable.EstimateNearestPrimeIndex(n);
      ulong prime = this[index];
      if (n > prime)
      {
        while (n > (prime = this[++index])) ;
      }
      else if (n < prime)
      {
        while (n < (prime = this[--index])) ;
      }
      return (n == prime);
    }

    private static List<ulong> mPrimes;

    private BitArray mSieve;
    private const int SIEVE_SIZE = 2097152;

    struct IndexLookupTable
    {
      // Approximation table to find index by prime number value
      private static long[] mSearchPrimeIndices = { 0, 1, 4, 8, 16, 34, 79, 183, 429, 1019, 2466, 6048 };
      private static long[] mSearchPrimeValues = { 2, 3, 11, 23, 59, 149, 409, 1097, 2999, 8111, 22027, 59879 };
      private static int mSearchPrimeMaxRow = mSearchPrimeIndices.Length - 1;
      private static long[] mSearchPrimeIntIndexDiffs = new long[mSearchPrimeMaxRow];
      private static long[] mSearchPrimeIntValueDiffs = new long[mSearchPrimeMaxRow];
      private static double mSearchPrimeExtRatio;

      static IndexLookupTable() {
        for (int i = 0; i < mSearchPrimeMaxRow; ++i)
        {
          mSearchPrimeIntIndexDiffs[i] = mSearchPrimeIndices[i + 1] - mSearchPrimeIndices[i];
          mSearchPrimeIntValueDiffs[i] = mSearchPrimeValues[i + 1] - mSearchPrimeValues[i];
        }
        mSearchPrimeExtRatio = mSearchPrimeIndices[mSearchPrimeMaxRow] / mSearchPrimeValues[mSearchPrimeMaxRow];
      }

      public static int EstimateNearestPrimeIndex(ulong n)
      {
        int tableIndex = (int)Math.Floor(Math.Log(n));
        int index = 0;
        if (tableIndex < mSearchPrimeMaxRow)
        {
          // Interpolation
          long tmp = (long)n - mSearchPrimeValues[tableIndex];
          tmp *= mSearchPrimeIntIndexDiffs[tableIndex];
          tmp /= mSearchPrimeIntValueDiffs[tableIndex];
          tmp += mSearchPrimeIndices[tableIndex];
          index = (int)tmp;
        }
        else
        {
          // Extrapolation
          index = (int)(n * mSearchPrimeExtRatio);
        }
        return index;
      }
    }
  }
}
