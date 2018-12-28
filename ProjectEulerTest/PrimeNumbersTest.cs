using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectEuler.Utils;

namespace ProjectEulerTest
{
  [TestClass]
  public class PrimeNumbersTest
  {
    [TestMethod]
    public void TestFirstPrimes()
    {
      PrimeNumbers primes = new PrimeNumbers();
      Assert.AreEqual(2UL, primes[0]);
      Assert.AreEqual(3UL, primes[1]);
      Assert.AreEqual(5UL, primes[2]);
      Assert.AreEqual(7UL, primes[3]);
      Assert.AreEqual(11UL, primes[4]);
      Assert.AreEqual(13UL, primes[5]);
      Assert.AreEqual(17UL, primes[6]);
      Assert.AreEqual(19UL, primes[7]);
      Assert.AreEqual(23UL, primes[8]);
      Assert.AreEqual(29UL, primes[9]);
    }

    [TestMethod]
    public void TestIsPrimeWithPrimes()
    {
      PrimeNumbers primes = new PrimeNumbers();
      for (int i = 1; i <= 100; ++i)
      {
        int index = i * 31;
        ulong prime = primes[index];
        Assert.AreEqual(true, primes.isPrime(prime));
      }
    }

    [TestMethod]
    public void TestIsPrimeWithNonPrimes()
    {
      PrimeNumbers primes = new PrimeNumbers();
      for (int i = 1; i <= 100; ++i)
      {
        int index = i * 37;
        ulong prime = primes[index];
        ulong nextPrime = primes[index + 1];
        for (ulong n = prime + 1; n < nextPrime; ++n)
          Assert.AreEqual(false, primes.isPrime(n));
      }
    }
  }
}
