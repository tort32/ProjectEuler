using System;
using System.Diagnostics;

namespace Utils
{
  /*
   * Rough execution time measurement routine.
   * 
   * Note such measurements may be irrelevent for
   * short intervals cause of OS context switching,
   * scheduling or instable due to uneven CPU load.
   */
  public static class TimeUtil
  {
    public static T Measure<T>(Func<T> action)
    {
      var watch = new Stopwatch();
      try
      {
        watch.Start();
        return action();
      }
      finally
      {
        watch.Stop();
        Console.WriteLine(watch.Elapsed);
      }
    }

    public static T Measure<T>(String msgFmt, Func<T> action)
    {
      var watch = new Stopwatch();
      try
      {
        watch.Start();
        return action();
      }
      finally
      {
        watch.Stop();
        Console.WriteLine(String.Format(msgFmt, watch.Elapsed));
      }
    }
  }
}
