using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Problems
{
  class Problem34 : ProblemBase
  {
    public ulong Solve()
    {
      ulong result = 0;
      uint[] cnt = new uint[6];
      cnt[0] = 3;
      while(true)
      {
        ulong num = ((((cnt[5] * 10 + cnt[4]) * 10 + cnt[3]) * 10 + cnt[2]) * 10 + cnt[1]) * 10 + cnt[0];
        ulong sum = 0;
        for (int j = 5; j >= 0; --j)
        {
          if (cnt[j] == 0 && sum == 0)
            continue;
          sum += FACT_TABLE[cnt[j]];
        }
        if (num == sum)
        {
          Console.Out.WriteLine("{0}", num);
          result += num;
        }
        // Increment counter
        ++cnt[0];
        for (int j = 0; j < 5; ++j)
        {
          if (cnt[j] < 10)
            break;
          cnt[j] = 0;
          ++cnt[j + 1];
        }
        if (cnt[5] == 10)
          break;
      };
      return result;
    }

    static Problem34()
    {
      FACT_TABLE = new ulong[10];
      for (int i = 0; i < 10; ++i)
      {
        FACT_TABLE[i] = (ulong) Utils.Factorial.Get(i);
      }
    }

    static ulong[] FACT_TABLE;
  }
}
