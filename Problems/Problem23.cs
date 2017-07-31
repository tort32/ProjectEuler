using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Utils;

namespace ProjectEuler.Problems
{
  class Problem23 : ProblemBase
  {
    public ulong Solve()
    {
      return Dividers.getSumOfDividers(1);
    }
  }
}
