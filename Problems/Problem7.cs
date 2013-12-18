using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Problems
{
    // 10001st prime
    // By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
    // What is the 10 001st prime number?

    class Problem7 : ProblemBase
    {
        const int MAX_COUNT = 10001;

        public Problem7()
        {
            /*int number = 2;
            while (mPrime.Count < MAX_COUNT)
            {
                int firstDevider = mPrime.FirstOrDefault(prime => number % prime == 0);
                if (firstDevider == 0)
                {
                    mPrime.Add(number);
                }
                ++number;
            }*/
        }

        public ulong Solve()
        {
            //Console.WriteLine(string.Join(", ", mPrime));
            return mPrime[MAX_COUNT-1];
        }

        //private List<int> mPrime = new List<int>(MAX_COUNT);
        private Utils.PrimeNumbers mPrime = new Utils.PrimeNumbers();
    }
}
