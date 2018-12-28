using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Utils;

namespace ProjectEuler.Problems
{
  class Problem25 : ProblemBase
  {
    public ulong Solve()
    {
      BigNumber fiPrev = new BigNumber(1024, 1);
      BigNumber fiCur = new BigNumber(1024, 1);
      ulong n = 2;
      do
      {
        BigNumber tmp = new BigNumber(fiCur);
        fiCur.Add(fiPrev);
        fiPrev = tmp;
        ++n;
        if(fiCur.GetDigitAt(999) > 0 || fiCur.GetDigitAt(1000) > 0 || fiCur.GetDigitAt(1001) > 0)
        {
          break;
        }
      } while (true);
      return n;
    }
  }
}
