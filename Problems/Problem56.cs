using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler;
using ProjectEuler.Utils;

internal class Problem56 : ProblemBase
{
  private const int DIGITS_COUNT = 200;

  public ulong Solve()
  {
    BigNumber[] n = new BigNumber[100];
    for (uint i = 0; i < n.Length; ++i)
      n[i] = new BigNumber(DIGITS_COUNT, i);

    uint maxSum = 0;
    //uint maxI = 0;
    //uint maxJ = 0;
    //BigNumber maxN = null;
    for (uint i = 1; i < n.Length; ++i)
    {
      uint startJ = GetStartJ(i);
      for (uint j = startJ; j < n.Length; ++j)
      {
        BigNumber m = n[i].Power(j);
        uint sum = m.GetSumOfDigits();
        if (sum > maxSum)
        {
          maxSum = sum;
          //maxI = i;
          //maxJ = j;
          //maxN = m;
        }
      }
    }
    //Console.WriteLine("{0}^{1} = {2}", maxI, maxJ, maxN);
    return maxSum;
  }

  private uint GetStartJ(uint i)
  {
    return (i < 85u) ? 85u : 1u;
  }
}
