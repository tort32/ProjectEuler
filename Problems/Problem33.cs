using System;
using Utils;

class Problem33 : ProblemBase
{
  public ulong Solve()
  {
    ulong mult1 = 1;
    ulong mult2 = 1;
    for (uint i = 1; i < 10; ++i)
    {
      for (uint j = 1; j < 10; ++j)
      {
        /*if (j < i)
          continue;*/
        for (uint k = 1; k < 10; ++k)
        {
          if (k <= i)
            continue;
          ulong n1 = 10 * i + j;
          ulong n2 = 10 * j + k;
          if (n1 * k == n2 * i)
          {
            mult1 *= n1;
            mult2 *= n2;
            Console.Out.WriteLine("{0}{1}/{1}{2} = {0}/{2}", i, j, k);
          }
        }
      }
    }
    ulong gcd = Dividers.GreatestCommonDivisor(mult1, mult2);
    ulong result = mult2 / gcd;
    return result;
  }
}
