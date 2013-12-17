using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problems
{
  // A Pythagorean triplet is a set of three natural numbers, a < b < c, for which, a^2 + b^2 = c^2
  // For example, 3^2 + 4^2 = 9 + 16 = 25 = 5^2.
  // There exists exactly one Pythagorean triplet for which a + b + c = 1000.
  // Find the product abc.

  class Problem9: ProblemBase
  {
    private const double EPSILON = 1e-6;
    public Decimal Solve()
    {
      Decimal aValue = 0, bValue = 0, cValue = 0;
      for (int a = 1; a < 1000; ++a )
      {
        double b = (double)(1000*1000 - 2000*a)/(double)(2000 - 2*a);
        if (Math.Abs(Math.Floor(b) - b) < EPSILON)
        {
          aValue = a;
          bValue = (Decimal)Math.Floor(b);
          break;
        }
      }
      cValue = 1000 - (aValue + bValue);

      return aValue*bValue*cValue;
    }
  }
}
