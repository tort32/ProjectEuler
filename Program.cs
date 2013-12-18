using System;
using ProjectEuler.Problems;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            ProblemBase problem = new Problem7();
            Decimal solution = problem.Solve();

            watch.Stop();
            double total = watch.Elapsed.TotalMilliseconds;
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Elapsed: {0}{1}\t\tSolution: {2}",
                watch.Elapsed, (total > 1000.0f) ? "  !!! TIMEOUT !!!": string.Empty, solution);
        }
    }
}
