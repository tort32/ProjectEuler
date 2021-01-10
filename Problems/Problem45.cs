using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ProjectEuler;
using ProjectEuler.Utils;

internal class Problem45 : ProblemBase
{
  const int TRI_MAX = 60000;
  const int PENT_MAX = TRI_MAX * 5 / 3;
  const int HEX_MAX = TRI_MAX / 2;

  public ulong Solve()
  {
    ulong[] tri = PolygonalNumbers.Triangular().Take(TRI_MAX).ToArray();
    ulong[] pent = PolygonalNumbers.Pentagonal().Take(PENT_MAX).ToArray();
    ulong[] hex = PolygonalNumbers.Hexagonal().Take(HEX_MAX).ToArray();

    var pentagonals = new SortedSet<ulong>(pent);
    var hexagonals = new SortedSet<ulong>(hex);

    ulong latest = 0;
    for (int n = 0; n < TRI_MAX; ++n)
    {
      ulong t = tri[n];
      if (pentagonals.Contains(t) && hexagonals.Contains(t))
      {
        Console.WriteLine(t);
        latest = t;
      }
    }
    return latest;
  }
}
