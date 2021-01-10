using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ProjectEuler;
using ProjectEuler.Utils;

internal class Problem44 : ProblemBase
{
  const int MAX = 3000;
  public ulong Solve()
  {
    ulong[] pent = PolygonalNumbers.Pentagonal().Take(MAX).ToArray();
    var pentagonals = new SortedSet<ulong>(pent);

    for (int m = 0; m < MAX - 2; ++m)
    {
      ulong pentM = pent[m]; // Minimal
      for (int k = 0; k < MAX - 1; ++k)
      {
        ulong pentK = pent[k];
        ulong sumMK = pentM + pentK;
        if (pentagonals.Contains(sumMK))
        {
          ulong pentJ = sumMK;
          ulong sumJK = pentJ + pentK;
          if (pentagonals.Contains(sumJK))
          {
            return pentM;
          }
        }
      }
    }
    return 0ul;
  }
}
