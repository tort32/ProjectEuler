using System.Collections.Generic;
using System.Linq;

namespace Utils
{
  class Factorial
  {
    private static List<long> table = new List<long>(10);

    public static long Get(int n)
    {
      if (n == 0)
        return 1;
      if (n > table.Count() - 1)
      {
        table.AddRange(Enumerable.Repeat(0L, n - table.Count() + 1));
      }
      if (table[n] != 0)
      {
        return table[n]; // Get from cache
      }
      else
      {
        long prev = Get(n - 1);
        long value = prev * n;
        table[n] = value; // Cache the result
        return value;
      }
    }
  }
}
