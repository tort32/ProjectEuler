using Utils;

// 10001st prime
// By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
// What is the 10 001st prime number?
class Problem7 : ProblemBase
{
  const int MAX_COUNT = 10001;

  public ulong Solve()
  {
    return (ulong)mPrime[MAX_COUNT - 1];
  }

  private PrimeNumbers mPrime = new PrimeNumbers();
}
