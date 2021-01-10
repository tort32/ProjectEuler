using System;
using System.Collections.Generic;

class Problem29 : ProblemBase
{
  public ulong Solve()
  {
    List<double> data = new List<double>();
    for (int a = 2; a < 101; ++a)
    {
      for (int b = 2; b < 101; ++b)
      {
        data.Add(b * Math.Log(a));
      }
    }
    data.Sort();

    ulong s = 1;
    for (int i = 0; i < data.Count - 1; ++i)
    {
      double cur = data[i];
      double next = data[i + 1];
      double ratio = (next - cur) / cur;
      if (ratio > 10e-12)
        ++s;
    }
    return s;
  }
}
