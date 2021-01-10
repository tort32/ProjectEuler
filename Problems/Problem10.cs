using System.Linq;
using Utils;

// The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
// Find the sum of all the primes below two million.
class Problem10 : ProblemBase
{
  private const ulong MAX_FOR_PRIME = 2000000U;

  public ulong Solve()
  {
    var gen = new PrimeNumbers();
    var items = gen.TakeWhile(prime => prime < MAX_FOR_PRIME);
    ulong sum = items.Aggregate<ulong, ulong>(0U, (result, item) => result + item);
    return sum;
  }
}
