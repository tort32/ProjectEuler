using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Utils
{
  class PolygonalNumbers
  {
    public static IEnumerable<ulong> Triangular()
    {
      uint n = 0;
      uint sum = 0;
      while (true)
        yield return sum += (++n);
    }

    public static IEnumerable<ulong> Pentagonal()
    {
      uint n = 0;
      while (true)
        yield return (++n) * (3 * n - 1) / 2;
    }

    public static IEnumerable<ulong> Hexagonal()
    {
      uint n = 0;
      while (true)
        yield return (++n) * (2 * n - 1);
    }
  }
}
