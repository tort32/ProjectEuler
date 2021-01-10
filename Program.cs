using System;
using System.Diagnostics;

class Program
{
  static void Main(string[] args)
  {
    var watch = new System.Diagnostics.Stopwatch();
    watch.Start();

    ProblemBase problem = new Problem56();
    Decimal solution = problem.Solve();

    watch.Stop();
    double total = watch.Elapsed.TotalMilliseconds;
    PrintLine("===============================================================================");
    PrintLine(String.Format("Elapsed: {0}{1}",
      watch.Elapsed, (total > 1000.0f) ? "  !!! TIMEOUT !!!" : string.Empty));
    PrintLine(String.Format("Solution: {0}", solution));
  }

  static void PrintLine(string msg)
  {
    Trace.WriteLine(msg);
    Console.WriteLine(msg);
  }
}
