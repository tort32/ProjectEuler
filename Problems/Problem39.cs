using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problems
{
  // If p is the perimeter of a right angle triangle with integral length sides, {a,b,c},
  // there are exactly three solutions for p = 120: {20,48,52}, {24,45,51}, {30,40,50}
  //
  // For which value of p ≤ 1000, is the number of solutions maximised?
  class Problem39 : ProblemBase
  {
    public ulong Solve()
    {
      int maxP = 1000;

      // Lookup for squares of integers
      Dictionary<int, int> squares = new Dictionary<int, int>();
      for (int i = 2; i < maxP / 2; ++i)
        squares.Add(i * i, i);

      // Enumerate triangles and accumulate solutions
      int[] solutions = new int[maxP + 1];
      for (int a = 3; a < maxP / 2 - 3; ++a)
      {
        int bMax = Math.Min(a, maxP - a);
        for (int b = 3; b <= bMax - 3; ++b)
        {
          int cSqr = a * a + b * b;
          if (squares.TryGetValue(cSqr, out int c))
          {
            int p = a + b + c;
            if (p <= maxP)
              ++solutions[p];
          }
        }
      }

      // Find P with maximum of solutions
      int maxSolutions = 0;
      int pOfMax = 0;
      for (int i = 5; i <= maxP; ++i)
      {
        if (solutions[i] > maxSolutions)
        {
          maxSolutions = solutions[i];
          pOfMax = i;
        }
      }

      return (ulong)pOfMax;
    }
  }
}
